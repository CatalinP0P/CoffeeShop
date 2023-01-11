using System;
namespace CoffeeShop.Models
{
	public class ShowOrdersViewModel
	{
		public ShowOrdersViewModel()
		{
			
		}

		public Order Orders { get; set; }

		public List<int> ProductIds { get; set; }

		public List<Product> Products { get; set; }
 
		public List<string> ProductImageUrl { get; set; }
	}
}

