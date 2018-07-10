namespace MadeLine.Api.Controllers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        //private ApplicationUser currentUser;

        public BaseController(IOptions<AppSettings> options)
        {
            this.AppSettings = options.Value;
        }

        protected AppSettings AppSettings { get; }

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