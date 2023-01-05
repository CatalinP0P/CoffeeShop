using System;
namespace CoffeeShop.Models
{
	public class CartViewModel
	{

		public CartViewModel()
		{

		}

		public Product Product { get; set; }

		public CartProducts CartProduct { get; set; }

	}
}

