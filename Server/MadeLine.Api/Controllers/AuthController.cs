namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.Auth;
    using MadeLine.Api.Extensions.Mvc;
    using MadeLine.Api.Helpers;
    using MadeLine.Api.ViewModels;
    using MadeLine.Api.ViewModels.Accounts;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private static readonly HttpClient Client = new HttpClient();

        public AuthController(IOptions<AppSettings> options, UserManager<ApplicationUser> userManager, IJwtFactory jwtFactory) : base(options)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = base.AppSettings.JwtIssuerOptions;
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
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>() { Errors = ModelState.GetErrors() });
            }

            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return new BadRequestObjectResult(new BadRequestViewModel<ModelStateError>() { Errors = ModelState.GetErrors() });
            }

            var jwt = await Token.GenerateJwt(
                identity, 
                _jwtFactory, 
                credentials.Email, 
                _jwtOptions, 
                new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(new OkObjectViewModel<ResponseTokenViewModel>() { Message = "Success", Data = jwt });
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
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

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

                var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

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
            var localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                return new BadRequestObjectResult(new { error = "Failed to create local user account." });
            }

            var jwt = await Token.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}