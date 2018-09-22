namespace MadeLine.Web.Config
{
    using MadeLine.Core.Managers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ManagersConfig
    {
        internal static void ConfigureAppManagers(IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IImageManager, ImageManager>();
            services.AddTransient<IProductCategoriesManager, ProductCategoriesManager>();
            services.AddTransient<IProductColorsManager, ProductColorsManager>();
            services.AddTransient<IProductSizesManager, ProductSizesManager>();
            services.AddTransient<IProductManager, ProductManager>();
        }
    }
}
