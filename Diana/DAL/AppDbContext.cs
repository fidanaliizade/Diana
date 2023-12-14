using Diana.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diana.DAL
{
	public class AppDbContext :IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<ProductMaterial> ProductMaterials { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<ProductSize> ProductSizes { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
	}
}
