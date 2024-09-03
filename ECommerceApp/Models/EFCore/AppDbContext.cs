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


		/*

				Adding seed data.	

		*/

		modelBuilder.Entity<Category>().HasData(
			new Category() { Name = "Seed Category", CategoryId=1, Products = [] }
		);

		modelBuilder.Entity<User>().HasData(
		new User()
		{
			UserId = 1,
			Username="user1",
			Address="i m e34",
			City="Baku",
			Country="Az",
			Email="elvincode1517@gmail.com",
			FirstName="Elvin",
			LastName="Siracli",
			Orders = new List<Order>(),
			PasswordHash="elvin123",
			PhoneNumber="+994515276567"
,
			PostalCode="Az-az",
			State="RandomState"
		}
	);

		modelBuilder.Entity<Cart>().HasData(
			new Cart() { CartItems= [], CartId = 1, UserId = 1 }
		);


		/*
		
		ProductImages = [new ProductImage(){
				ImageUrl = @"D:\Games\bmw1.jpg",
				IsMainImage = true
			},
			new ProductImage()
			{
				ImageUrl = @"D:\Games\bmw2.jpg",
				IsMainImage = false
			},new ProductImage()
			{
				ImageUrl = @"D:\Games\bmw3.jpg",
				IsMainImage = false,
			}, new ProductImage()
			{
				ImageUrl = @"D:\Games\bmw5.jpg",
				IsMainImage = false
			}, new ProductImage()
			{
				ImageUrl = @"D:\Games\bmw8.jpg",
				IsMainImage = false
			}],
				ProductReviews = [new ProductReview()
			{
				Rating=5,
				Review="Good",
				UserId =1,
				DateCreated=DateTime.Now,
			},
			new ProductReview()
			{
				Rating=4,
				Review="Normal",
				UserId =1,
				DateCreated=DateTime.Now,
			}]
		
		*/


		modelBuilder.Entity<Product>().HasData(
			new Product()
			{
				CategoryId = 1,
				ProductId = 1,
				Description="Seed Description 1",
				IsActive=true,
				Name="Seed Product Name 1",
				Price = 99.99M,
				StockQuantity=100,
				DateAdded=DateTime.Now,
				ProductImages= [],
				ProductReviews= []

			},
			new Product()
			{
				CategoryId = 1,
				ProductId = 2,
				Description="Seed Desion 222",
				IsActive=true,
				Name="Seed2 Name2 222",
				Price = 230.5M,
				DateAdded=DateTime.Now,
				StockQuantity=100,
				ProductImages= [],
				ProductReviews= []
			},
			new Product()
			{
				CategoryId = 1,
				ProductId = 3,
				Description="Seed 33333333",
				IsActive=true,
				StockQuantity=100,
				Name="Seed33333333",
				Price = 75,
				DateAdded=DateTime.Now,
				ProductImages= [],
				ProductReviews= []
			},
			new Product()
			{
				CategoryId = 1,
				StockQuantity=100,
				ProductId = 4,
				Description="S Product 4444",
				IsActive=true,
				Name="Seed4 Name44",
				Price = 44.9M,
				DateAdded=DateTime.Now,
				ProductImages= [],
				ProductReviews= []
			}
		);


		modelBuilder.Entity<ProductImage>().HasData(
			new ProductImage() { ProductImageId = 1, ProductId = 1, ImageUrl = @"D:\Games\bmw1.jpg", IsMainImage = true },
			new ProductImage() { ProductImageId = 2, ProductId = 1, ImageUrl = @"D:\Games\bmw2.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 3, ProductId = 1, ImageUrl = @"D:\Games\bmw3.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 4, ProductId = 1, ImageUrl = @"D:\Games\bmw5.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 5, ProductId = 1, ImageUrl = @"D:\Games\bmw8.jpg", IsMainImage = false },


			new ProductImage() { ProductImageId = 6, ProductId = 2, ImageUrl = @"D:\Games\bmw1.jpg", IsMainImage = true },
			new ProductImage() { ProductImageId = 7, ProductId = 2, ImageUrl = @"D:\Games\bmw2.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 8, ProductId = 2, ImageUrl = @"D:\Games\bmw3.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 9, ProductId = 2, ImageUrl = @"D:\Games\bmw5.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 10, ProductId =2, ImageUrl = @"D:\Games\bmw8.jpg", IsMainImage = false },


			new ProductImage() { ProductImageId = 11, ProductId = 3, ImageUrl = @"D:\Games\bmw1.jpg", IsMainImage = true },
			new ProductImage() { ProductImageId = 12, ProductId = 3, ImageUrl = @"D:\Games\bmw2.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 13, ProductId = 3, ImageUrl = @"D:\Games\bmw3.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 14, ProductId = 3, ImageUrl = @"D:\Games\bmw5.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 15, ProductId = 3, ImageUrl = @"D:\Games\bmw8.jpg", IsMainImage = false },


			new ProductImage() { ProductImageId = 16, ProductId = 4, ImageUrl = @"D:\Games\bmw1.jpg", IsMainImage = true },
			new ProductImage() { ProductImageId = 17, ProductId = 4, ImageUrl = @"D:\Games\bmw2.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 18, ProductId = 4, ImageUrl = @"D:\Games\bmw3.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 19, ProductId = 4, ImageUrl = @"D:\Games\bmw5.jpg", IsMainImage = false },
			new ProductImage() { ProductImageId = 20, ProductId = 4, ImageUrl = @"D:\Games\bmw8.jpg", IsMainImage = false }
		);


		modelBuilder.Entity<ProductReview>().HasData(
			new ProductReview()
			{
				ProductReviewId = 1,
				Rating=1,
				Review="Very Bad",
				UserId =1,
				DateCreated=DateTime.Now,
				ProductId=1
			},
			new ProductReview()
			{
				ProductReviewId = 2,
				Rating=4,
				Review="Good",
				UserId =1,
				DateCreated=DateTime.Now,
				ProductId=1
			}
		);
	}
}
