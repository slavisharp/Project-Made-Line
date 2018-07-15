namespace MadeLine.Core
{
    using MadeLine.Core.Settings;
    using MadeLine.Data;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DbInitializer
    {
        public static void SeedDatabase(
            ApplicationDbContext context, 
            AppSettings settings,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(context, roleManager, settings);
        }

        private static void SeedRoles(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, AppSettings settings)
        {
            var roles = new HashSet<string>(context.Roles.Select(r => r.Name).ToList());
            AddRole(roleManager, settings.UserRole, roles);
            AddRole(roleManager, settings.AdminRole, roles);
            AddRole(roleManager, settings.BrandRole, roles);
        }

        private static void AddRole(RoleManager<IdentityRole> roleManager, string role, HashSet<string> roles)
        {
            if (!roles.Contains(role))
            {
                roleManager.CreateAsync(new IdentityRole()
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role
                }).Wait();
            }
        }
    }
}
