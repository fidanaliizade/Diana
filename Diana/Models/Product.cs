using Diana.Models.Entity;
using System.Drawing;

namespace Diana.Models
{
	public class Product:BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public bool IsDeleted { get; set; }
		public Category? Category { get; set; }
		public List<Material>? ProductMaterials { get; set; }
		public List<Color>? ProductColors { get; set; }
		public List<Size>? ProductSizes { get; set; }
		public List<ProductImage>? ProductImages { get; set; }
	}
}
