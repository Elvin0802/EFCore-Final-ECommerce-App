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

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(App.Configuration!.GetConnectionString("DefaultConnection"));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{




	}


}
