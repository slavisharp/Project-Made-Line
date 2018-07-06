namespace MadeLine.Core.Managers
{
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class AccountManager : IAccountManager
    {
        private IRepository<ApplicationUser> userRepo;
        private UserManager<ApplicationUser> userManager;

        public AccountManager(IRepository<ApplicationUser> repo, UserManager<ApplicationUser> userManager)
        {
            this.userRepo = repo;
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> RegisterUserAsync(IRegisterModel model)
        {
            var userIdentity = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                RegisterIP = model.RegisterIP
            };

            return await this.userManager.CreateAsync(userIdentity, model.Password);
        }
    }
}
