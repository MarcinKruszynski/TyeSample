using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProductService.Data.EntityConfigurations;
using ProductService.Model;

namespace ProductService.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<ProductItem> ProductItems { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            builder.ApplyConfiguration(new ProductItemEntityTypeConfiguration());
        }
    }

    public class ProductContextDesignFactory: IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<ProductContext>()
                .UseNpgsql("Host=127.0.0.1;Port=5434;Database=productdb;Username=postgres;Password=password");

            return new ProductContext(optionsBuilder.Options);
        }
    }
}
