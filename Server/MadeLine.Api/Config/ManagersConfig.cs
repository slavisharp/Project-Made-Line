namespace MadeLine.Web.Config
{
    using MadeLine.Core.Managers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ManagersConfig
    {
        internal static void ConfigureAppManagers(IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            //services.AddTransient<IHomePageManager, HomePageManager>();
            //services.AddTransient<IBlogPostManager, BlogPostManager>();
            //services.AddTransient<IQuoteManager, QuoteManager>();
            //services.AddTransient<IImageManager, ImageManager>();
            //services.AddTransient<IOrderManager, OrderManager>();
            //services.AddTransient<ICartManager, CartManager>();
            services.AddTransient<IProductManager, ProductManager>();
        }
    }
}
