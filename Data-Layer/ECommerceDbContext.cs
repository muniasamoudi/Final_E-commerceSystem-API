using Data_Layer.DataModel;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem_API.Data_Layer
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Products Table
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(p => p.productID);
            modelBuilder.Entity<Product>().Property(p => p.productName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.productPrice).IsRequired().HasColumnType("decimal(18,2)");

            // Configure the Customers Table
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Customer>()
            .HasOne(c => c.Cart)    // Customer has one ShoppingCart
            .WithOne(s => s.Owner) // ShoppingCart has one Customer as owner
            .HasForeignKey<ShoppingCart>(s => s.OwnerId); // Define the foreign key in ShoppingCart

           
            // Configure the Order Table
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(o => o.orderID);
            modelBuilder.Entity<Order>().Property(o => o.orderDate).IsRequired();
            modelBuilder.Entity<Order>().HasOne(o => o.customer).WithMany(c => c.orders).HasForeignKey(o => o.customerID);
            modelBuilder.Entity<Order>().HasMany(o => o.products).WithMany(p => p.orders); 

            // Configure the ShoppingCart Table
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCarts");
            modelBuilder.Entity<ShoppingCart>().HasKey(s => s.Id);
            // Configure the one-to-one relationship between Customer and ShoppingCart
            modelBuilder.Entity<ShoppingCart>().HasOne(s => s.Owner).WithOne(c => c.Cart).HasForeignKey<ShoppingCart>(s => s.OwnerId);
            // Configure the one-to-many relationship between ShoppingCart and Product
            modelBuilder.Entity<ShoppingCart>().HasMany(s => s.Products).WithOne();

            
            base.OnModelCreating(modelBuilder);
        }
    
    }
}
