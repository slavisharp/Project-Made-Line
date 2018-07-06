namespace MadeLine.Web.Config
{
    using MadeLine.Data;
    using MadeLine.Data.Models;
    using MadeLine.Data.Repository;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    internal static class DataServicesConfig
    {
        internal static void ConfigureDataServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            var builder = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
