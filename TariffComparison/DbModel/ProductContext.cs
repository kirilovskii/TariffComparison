using Microsoft.EntityFrameworkCore;

namespace TariffComparison.DbModel
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public string DbPath { get; }      
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData
                (
                    new Product
                    {
                        ProductId = 1,
                        Name = "Basic electricity tariff",
                        ProductType = ProductType.Basic,
                        UnconditionalCosts = 5m,
                        ConsumptionCosts = 0.22m
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "Packaged tariff",
                        ProductType = ProductType.Packaged,
                        PackageCosts = 800m,
                        InclidedInPackage = 4000,
                        ConsumptionCosts = 0.3m
                    }
                );
        }
    }
}