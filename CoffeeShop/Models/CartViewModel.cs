using System;
namespace CoffeeShop.Models
{
	public class CartViewModel
	{

		public CartViewModel()
		{

		}

		public string UserId { get; set; }

		public Product Product { get; set; }

		public CartProducts CartProduct { get; set; }

	}
}

