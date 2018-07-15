namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IAccountManager
    {
        UserManager<ApplicationUser> UserManager { get; }

        Task<IdentityResult> RegisterUserAsync(IRegisterModel model);

        Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);

        IEnumerable<string> GetUserRoleNames(ApplicationUser userToVerify);
    }
}
