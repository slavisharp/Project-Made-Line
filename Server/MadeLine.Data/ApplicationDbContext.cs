namespace MadeLine.Data
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;
    using MadeLine.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<BrandTranslation> BrandTranslations { get; set; }

        public virtual DbSet<Campaign> Campaigns { get; set; }

        public virtual DbSet<CampaignTranslation> CampaignTranslations { get; set; }

        public virtual DbSet<CampaignProduct> CampaignProducts { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductTranslation> ProductTranslations { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<ProductColor> ProductColors { get; set; }

        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<ProductSize> ProductSizes { get; set; }

        public virtual DbSet<Size> Sizes { get; set; }

        public virtual DbSet<Vlog> Vlogs { get; set; }

        public virtual DbSet<VlogTranslation> VlogTranslations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            BuildProduct(builder);
            BuildAddress(builder);
            BuildCampaign(builder);
        }

        private static void BuildAddress(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasMany(a => a.BillingOrders)
                .WithOne(o => o.BillingAddress)
                .HasForeignKey(o => o.BillingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Address>()
                .HasMany(a => a.ShippigOrders)
                .WithOne(o => o.ShippingAddress)
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void BuildCampaign(ModelBuilder builder)
        {
            builder.Entity<CampaignProduct>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Campaings)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CampaignProduct>()
                .HasOne(c => c.Campaign)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void BuildProduct(ModelBuilder builder)
        {
            // Product <-> Image
            builder.Entity<ProductImage>()
                .HasKey(ci => new { ci.ProductId, ci.ImageId });
            builder.Entity<ProductImage>()
                .HasOne(ci => ci.Image)
                .WithMany(i => i.ProductImages)
                .HasForeignKey(ci => ci.ImageId)
                .OnDelete(DeleteBehavior.Restrict); ;
            builder.Entity<ProductImage>()
                .HasOne(ci => ci.Product)
                .WithMany(i => i.Images)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Product <-> Category
            builder.Entity<ProductCategory>()
                .HasKey(ci => new { ci.ProductId, ci.CategoryId });
            builder.Entity<ProductCategory>()
                .HasOne(ci => ci.Category)
                .WithMany(i => i.Products)
                .HasForeignKey(ci => ci.CategoryId);
            builder.Entity<ProductCategory>()
                .HasOne(ci => ci.Product)
                .WithMany(i => i.Categories)
                .HasForeignKey(ci => ci.ProductId);
            
            builder.Entity<Product>()
                .HasOne(o => o.MainImage)
                .WithMany(o => o.ProductMainImages)
                .HasForeignKey(o => o.MainImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(o => o.HighlightImage)
                .WithMany(o => o.ProductHighlightedImages)
                .HasForeignKey(o => o.HighlightImageId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Product>()
                .HasIndex(p => p.Alias)
                .IsUnique(true);

            builder.Entity<Product>()
                .HasIndex(p => p.IsHighlighted);

            builder.Entity<Product>()
                .HasIndex(p => p.SKUCode);          
        }

        #region Save Changes
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            var entries = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));
            foreach (var entry in entries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.Created == default(DateTime))
                {
                    entity.Created = DateTime.UtcNow;
                }
                else
                {
                    entity.Modified = DateTime.UtcNow;
                }
            }
        }
        #endregion
    }
}
