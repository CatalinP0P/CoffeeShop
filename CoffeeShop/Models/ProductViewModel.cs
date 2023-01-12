using System;
using CoffeeShop.Models;

namespace CoffeeShop.Models
{
	public class ProductViewModel
	{
		public ProductViewModel()
		{

		}

		public Product Product { get; set; }

		public IEnumerable<string> Categories = new string[]
		{
			"Beveradge",
			"Food",
			"Entertainment"
		};
	} 
}

