﻿using Diana.Models.Entity;
using System.Drawing;

namespace Diana.Models
{
	public class Product:BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
		public List<ProductMaterial>? ProductMaterials { get; set; }
		public List<ProductColor>? ProductColors { get; set; }
		public List<ProductSize>? ProductSizes { get; set; }
		public List<ProductImage>? ProductImages { get; set; }
	}
}
