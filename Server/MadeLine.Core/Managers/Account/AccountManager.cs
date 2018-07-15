namespace MadeLine.Core.Managers
{
    using MadeLine.Core.Settings;
    using MadeLine.Data;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class AccountManager : IAccountManager
    {
        private IRepository<ApplicationUser> userRepo;
        private UserManager<ApplicationUser> userManager;
        private AppSettings settings;

        public AccountManager(
            IRepository<ApplicationUser> repo, 
            UserManager<ApplicationUser> userManager, 
            IOptions<AppSettings> options)
        {
            this.userRepo = repo;
            this.userManager = userManager;
            this.settings = options.Value;
        }

        public UserManager<ApplicationUser> UserManager { get { return this.userManager; } }

        public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await this.userManager.GetUserAsync(claimsPrincipal);
        }

        public IEnumerable<string> GetUserRoleNames(ApplicationUser user)
        {
            var context = this.userRepo.Context as ApplicationDbContext;
            var roleIds = context.UserRoles.Where(r => r.UserId == user.Id).Select(r => r.RoleId);
            var roleNames = context.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.Name);
            return roleNames;
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

            var registerResult = await this.userManager.CreateAsync(userIdentity, model.Password);
            if (registerResult.Succeeded)
            {
                //await this.userManager.AddToRoleAsync(userIdentity, settings.UserRole);
                var context = this.userRepo.Context as ApplicationDbContext;
                string roleId = context.Roles.Where(r => r.Name == settings.UserRole).Select(r => r.Id).FirstOrDefault();
                await context.UserRoles.AddAsync(new IdentityUserRole<string>()
                {
                    RoleId = roleId,
                    UserId = userIdentity.Id
                });

                await this.userRepo.SaveAsync();
            }
                    
            return registerResult;
        }
    }
}
