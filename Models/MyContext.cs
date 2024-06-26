using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ShoppyMcShopFace.Models
{
    public class MyContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<InvoiceLineItems> InvoiceLineItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInOrder> ProductsInOrders { get; set; }
        // public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between User and Order
            modelBuilder.Entity<User>()
                .HasMany(u => u.OrdersUserPlaced)
                .WithOne(o => o.OrderingUser)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust as needed

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderingUser)
                .WithMany(u => u.OrdersUserPlaced)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust as needed

            // Configure the relationship for the ShoppingCart
            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne()
                .HasForeignKey<Order>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust as needed
        }
    }
}
