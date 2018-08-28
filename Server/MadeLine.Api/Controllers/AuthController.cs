namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.Auth;
    using MadeLine.Api.Helpers;
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Accounts;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        //private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountManager accountManager;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;
        private static readonly HttpClient Client = new HttpClient();

        public AuthController(
            IOptions<AppSettings> options,
            IAccountManager accountManager,
            IJwtFactory jwtFactory) : base(options)
        {
            //this.userManager = accountManager.UserManager;
            this.accountManager = accountManager;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = base.AppSettings.JwtIssuerOptions;
        }

        // POST api/auth/login
        /// <summary>
        /// Logs in user
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>Security token</returns>
        [HttpPost("login")]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel<ResponseTokenViewModel>))]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<ModelStateError>))]
        public async Task<ActionResult<OkObjectViewModel>> Post([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>(ModelState.GetErrors()));
            }

            var jwt = await Token.GenerateJwt(
                identity, 
                jwtFactory, 
                credentials.Email, 
                jwtOptions, 
                new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(new OkObjectViewModel<ResponseTokenViewModel>("Success", jwt));
        }

        // POST api/auth/facebook
        [HttpPost("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={base.AppSettings.FacebookAuthSettings.AppId}&client_secret={base.AppSettings.FacebookAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return new BadRequestObjectResult(new { error = "Invalid facebook token." });
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v3.0/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await accountManager.UserManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await accountManager.UserManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded)
                {
                    var sb = new StringBuilder();
                    foreach (var item in result.Errors)
                    {
                        sb.Append($"{item.Code}: {item.Description}.");
                    }

                    return new BadRequestObjectResult(new { error = sb.ToString() });
                }
            }

            // generate the jwt for the local user...
            var localUser = await accountManager.UserManager.FindByNameAsync(userInfo.Email);
            //var roles = await _userManager.GetRolesAsync(localUser);  ---- NOT WORKING ?!?!?
            IEnumerable<string> roles = accountManager.GetUserRoleNames(localUser); ;

            if (localUser == null)
            {
                return new BadRequestObjectResult(new { error = "Failed to create local user account." });
            }

            var jwt = await Token.GenerateJwt(
                jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id, roles),
                jwtFactory, 
                localUser.UserName, 
                jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await accountManager.UserManager.FindByNameAsync(userName);
            //var roles = await _userManager.GetRolesAsync(userToVerify); ---- NOT WORKING ?!?!
            IEnumerable<string> roles = accountManager.GetUserRoleNames(userToVerify);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await accountManager.UserManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, roles));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}