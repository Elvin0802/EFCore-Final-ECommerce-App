using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceApp.Models.EFCore;

public class AppDbContext : DbContext
{
	public AppDbContext()
	{
		Database.EnsureCreated();
	}
	public DbSet<User> Users { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
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
		// User entity configuration
		modelBuilder.Entity<User>(u =>
		{
			u.HasKey(u => u.UserId);
			u.Property(u => u.UserId).ValueGeneratedOnAdd().IsRequired();
			u.Property(u => u.Username).IsRequired().HasMaxLength(50);
			u.Property(u => u.Email).IsRequired().HasMaxLength(100);
			u.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
			u.Property(u => u.Role).IsRequired();
			u.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
			u.Property(u => u.LastName).IsRequired().HasMaxLength(50);
			u.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(15);
			u.Property(u => u.Address).IsRequired().HasMaxLength(200);

			// One-to-many relationship: User - Orders
			u.HasMany(u => u.Orders)
			 .WithOne(o => o.User)
			 .HasForeignKey(o => o.UserId)
			 .OnDelete(DeleteBehavior.Cascade);

			// One-to-one relationship: User - Cart
			u.HasOne(u => u.Cart)
			 .WithOne(c => c.User)
			 .HasForeignKey<Cart>(c => c.UserId)
			 .OnDelete(DeleteBehavior.Cascade);
		});

		// Cart entity configuration
		modelBuilder.Entity<Cart>(c =>
		{
			c.HasKey(c => c.CartId);
			c.Property(c => c.CartId).ValueGeneratedOnAdd().IsRequired();
			c.Property(c => c.UserId).IsRequired();

			// One-to-many relationship: Cart - CartItems
			c.HasMany(c => c.CartItems)
			 .WithOne(ci => ci.Cart)
			 .HasForeignKey(ci => ci.CartId)
			 .OnDelete(DeleteBehavior.Cascade);
		});

		// CartItem entity configuration
		modelBuilder.Entity<CartItem>(ci =>
		{
			ci.HasKey(ci => ci.CartItemId);
			ci.Property(ci => ci.CartItemId).ValueGeneratedOnAdd().IsRequired();
			ci.Property(ci => ci.Quantity).IsRequired().HasDefaultValue(1);

			ci.HasOne(ci => ci.Cart)
			  .WithMany(c => c.CartItems)
			  .HasForeignKey(ci => ci.CartId)
			  .OnDelete(DeleteBehavior.Cascade);

			ci.HasOne(ci => ci.Product)
			  .WithMany()
			  .HasForeignKey(ci => ci.ProductId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Category entity configuration
		modelBuilder.Entity<Category>(c =>
		{
			c.HasKey(c => c.CategoryId);
			c.Property(c => c.CategoryId).ValueGeneratedOnAdd().IsRequired();
			c.Property(c => c.Name).IsRequired().HasMaxLength(100);

			// One-to-many relationship: Category - Products
			c.HasMany(c => c.Products)
			 .WithOne(p => p.Category)
			 .HasForeignKey(p => p.CategoryId)
			 .OnDelete(DeleteBehavior.Cascade);
		});

		// Product entity configuration
		modelBuilder.Entity<Product>(p =>
		{
			p.HasKey(p => p.ProductId);
			p.Property(p => p.ProductId).ValueGeneratedOnAdd().IsRequired();
			p.Property(p => p.Name).IsRequired().HasMaxLength(100);
			p.Property(p => p.Description).IsRequired().HasMaxLength(1000);
			p.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
			p.Property(p => p.StockQuantity).IsRequired().HasDefaultValue(0);
			p.Property(p => p.DateAdded).IsRequired();

			// One-to-many relationship: Product - ProductImages
			p.HasMany(p => p.ProductImages)
			 .WithOne(pi => pi.Product)
			 .HasForeignKey(pi => pi.ProductId)
			 .OnDelete(DeleteBehavior.Cascade);
		});

		// ProductImage entity configuration
		modelBuilder.Entity<ProductImage>(pi =>
		{
			pi.HasKey(pi => pi.ProductImageId);
			pi.Property(pi => pi.ProductImageId).ValueGeneratedOnAdd().IsRequired();
			pi.Property(pi => pi.ImageUrl).IsRequired().HasMaxLength(255);

			pi.HasOne(pi => pi.Product)
			  .WithMany(p => p.ProductImages)
			  .HasForeignKey(pi => pi.ProductId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Order entity configuration
		modelBuilder.Entity<Order>(o =>
		{
			o.HasKey(o => o.OrderId);
			o.Property(o => o.OrderId).ValueGeneratedOnAdd().IsRequired();
			o.Property(o => o.UserId).IsRequired();
			o.Property(o => o.OrderDate).IsRequired();
			o.Property(o => o.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
			o.Property(o => o.OrderStatus).IsRequired().HasMaxLength(50);

			// One-to-many relationship: Order - OrderItems
			o.HasMany(o => o.OrderItems)
			 .WithOne(oi => oi.Order)
			 .HasForeignKey(oi => oi.OrderId)
			 .OnDelete(DeleteBehavior.Cascade);

			// One-to-one relationship: Order - Payment
			o.HasOne(o => o.Payment)
			 .WithOne(p => p.Order)
			 .HasForeignKey<Payment>(p => p.OrderId)
			 .OnDelete(DeleteBehavior.Cascade);
		});

		// OrderItem entity configuration
		modelBuilder.Entity<OrderItem>(oi =>
		{
			oi.HasKey(oi => oi.OrderItemId);
			oi.Property(oi => oi.OrderItemId).ValueGeneratedOnAdd().IsRequired();
			oi.Property(oi => oi.Quantity).IsRequired();
			oi.Property(oi => oi.Price).IsRequired().HasColumnType("decimal(18,2)");

			oi.HasOne(oi => oi.Order)
			  .WithMany(o => o.OrderItems)
			  .HasForeignKey(oi => oi.OrderId)
			  .OnDelete(DeleteBehavior.Cascade);

			oi.HasOne(oi => oi.Product)
			  .WithMany()
			  .HasForeignKey(oi => oi.ProductId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Payment entity configuration
		modelBuilder.Entity<Payment>(p =>
		{
			p.HasKey(p => p.PaymentId);
			p.Property(p => p.PaymentId).ValueGeneratedOnAdd().IsRequired();
			p.Property(p => p.PaymentDate).IsRequired();
			p.Property(p => p.PaymentAmount).IsRequired().HasColumnType("decimal(18,2)");
			p.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(50);

			p.HasOne(p => p.Order)
			  .WithOne(o => o.Payment)
			  .HasForeignKey<Payment>(p => p.OrderId)
			  .OnDelete(DeleteBehavior.Cascade);
		});

		// Check constraints
		modelBuilder.Entity<User>().ToTable(t => t.HasCheckConstraint("CK_User_Role", "[Role] IN (0, 1)"));

		modelBuilder.Entity<CartItem>().ToTable(t => t.HasCheckConstraint("CK_CartItem_Quantity", "[Quantity] > 0"));

		modelBuilder.Entity<Product>().ToTable(t => t.HasCheckConstraint("CK_Product_Price", "[Price] > 0"));
		modelBuilder.Entity<Product>().ToTable(t => t.HasCheckConstraint("CK_Product_StockQuantity", "[StockQuantity] >= 0"));

		modelBuilder.Entity<OrderItem>().ToTable(t => t.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] > 0"));
		modelBuilder.Entity<OrderItem>().ToTable(t => t.HasCheckConstraint("CK_OrderItem_Price", "[Price] > 0"));

		modelBuilder.Entity<Payment>().ToTable(t => t.HasCheckConstraint("CK_Payment_PaymentAmount", "[PaymentAmount] > 0"));
	}

}
