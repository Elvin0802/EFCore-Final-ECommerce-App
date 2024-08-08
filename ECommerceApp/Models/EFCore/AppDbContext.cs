using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceApp.Models.EFCore;

public class AppDbContext : DbContext
{
	public AppDbContext()
	{

		Database.EnsureDeleted();
		Database.EnsureCreated();
	}
	public DbSet<User> Users { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
	public DbSet<ProductReview> ProductReviews { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Cart> Carts { get; set; }
	public DbSet<CartItem> CartItems { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(App.Configuration!.GetConnectionString("DefaultConnection"));
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure one-to-one relationships between User and Cart
		modelBuilder.Entity<User>()
			.HasOne(u => u.Cart)
			.WithOne(c => c.User)
			.HasForeignKey<Cart>(c => c.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Category>()
			.HasMany(c => c.Products)        // A Category has many Products
			.WithOne(p => p.Category)        // A Product belongs to one Category
			.HasForeignKey(p => p.CategoryId) // The foreign key on Product
			.OnDelete(DeleteBehavior.Cascade);

		// Configure one-to-one relationship between Order and Payment
		modelBuilder.Entity<Order>()
			.HasOne(o => o.Payment)
			.WithOne(p => p.Order)
			.HasForeignKey<Payment>(p => p.OrderId)
			.OnDelete(DeleteBehavior.Cascade);

		// Configure one-to-many relationship between Order and OrderItems
		modelBuilder.Entity<Order>()
			.HasMany(o => o.OrderItems)
			.WithOne(oi => oi.Order)
			.HasForeignKey(oi => oi.OrderId)
			.OnDelete(DeleteBehavior.Cascade);

		// Configure one-to-many relationship between Product and ProductImages
		modelBuilder.Entity<Product>()
			.HasMany(p => p.ProductImages)
			.WithOne(pi => pi.Product)
			.HasForeignKey(pi => pi.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		// Configure one-to-many relationship between Product and ProductReviews
		modelBuilder.Entity<Product>()
			.HasMany(p => p.ProductReviews)
			.WithOne(pr => pr.Product)
			.HasForeignKey(pr => pr.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		// Configure one-to-many relationship between User and ProductReviews
		modelBuilder.Entity<User>()
			.HasMany(u => u.ProductReviews)
			.WithOne(pr => pr.User)
			.HasForeignKey(pr => pr.UserId)
			.OnDelete(DeleteBehavior.Cascade);


		// Configure one-to-many relationship between Cart and CartItems
		modelBuilder.Entity<Cart>()
			.HasMany(c => c.CartItems)
			.WithOne(ci => ci.Cart)
			.HasForeignKey(ci => ci.CartId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
