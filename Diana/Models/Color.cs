using Diana.Models.Entity;

namespace Diana.Models
{
	public class Color:BaseEntity
	{
		public string Name { get; set; }
		public List<ProductColor> ProductColors { get; set; }
	}
}
