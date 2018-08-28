namespace MadeLine.Api.Controllers
{
    using MadeLine.Core.Managers;
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        //private ApplicationUser currentUser;

        public BaseController(IOptions<AppSettings> options)
        {
            this.AppSettings = options.Value;
        }

        protected AppSettings AppSettings { get; }
        
        protected void AddManagerErrorsToModelState(IEnumerable<IErrorResultModel> errors)
        {
            foreach (var item in errors)
            {
                ModelState.AddModelError(item.Code, item.Message);
            }
        }

        protected void AddIdentityErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var item in errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }

        //protected async Task<ApplicationUser> GetCurrentUserAsync()
        //{
        //    if (currentUser == null)
        //    {
        //        currentUser = await this.userManager.GetUserAsync(this.User);
        //    }

        //    return currentUser;
        //}
    }
}