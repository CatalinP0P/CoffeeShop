using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
	public class Product
	{
		public Product()
		{

		}

		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public int Stock { get; set; }

		[Required]
		public int Price { get; set; }

		[Required]
		public string ImageURL { get; set; }
	}
}

