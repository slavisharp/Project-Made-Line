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
    [ApiController]
    public class AccountsController : BaseController
    {
        private IAccountManager manager;

        public AccountsController(IOptions<AppSettings> options, IAccountManager accountManager) : base(options)
        {
            this.manager = accountManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
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
                return new OkObjectResult(new { message = "Account created" });
            }
            else
            {
                return new BadRequestObjectResult(new { errors = result.Errors });
            }
        }
    }
}
