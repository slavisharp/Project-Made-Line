namespace MadeLine.Api.Controllers
{
    using MadeLine.Api.ViewModels;
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class AccountsController : BaseController
    {
        private IAccountManager manager;

        public AccountsController(IOptions<AppSettings> options, IAccountManager accountManager) : base(options)
        {
            this.manager = accountManager;
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Message</returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(OkObjectViewModel))]
        [ProducesResponseType(statusCode: 400, Type = typeof(BadRequestViewModel<IdentityError>))]
        public async Task<ActionResult<OkObjectViewModel>> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = null;
            model.RegisterIP = this.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                result = await this.manager.RegisterUserAsync(model);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            
            if (result.Succeeded)
            {
                return new OkObjectResult(new OkObjectViewModel { Message = "Account created" });
            }
            else
            {
                return new BadRequestObjectResult(new BadRequestViewModel<IdentityError> () { Errors = result.Errors });
            }
        }
    }
}
