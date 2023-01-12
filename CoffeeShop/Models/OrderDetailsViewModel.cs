using System;
namespace CoffeeShop.Models
{
	public class OrderDetailsViewModel
	{
		public OrderDetailsViewModel()
		{
		}

		public Order Order { get; set; }

		public List<Product> Products { get; set; }

	}
}

