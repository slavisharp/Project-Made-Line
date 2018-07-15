namespace MadeLine.Api
{
    using MadeLine.Core.Settings;
    using MadeLine.Web.Config;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("AppSettings");
            AppSettings parsedSettings = new AppSettings();
            ConfigurationBinder.Bind(appSettings, parsedSettings);

            services.Configure<AppSettings>(appSettings);
            services.AddMemoryCache();
            DataServicesConfig.ConfigureDataServices(services, Configuration, parsedSettings);
            WebServicesConfig.ConfigureWebServices(services);
            UserServicesConfig.ConfigureAppServices(services);
            ManagersConfig.ConfigureAppManagers(services);
            AuthServicesConfig.ConfigJwtAuthentication(services, parsedSettings, Configuration["JWTKey"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Made Line API");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
