namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IAccountManager
    {
        Task<IdentityResult> RegisterUserAsync(IRegisterModel model);

        Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);
    }
}
