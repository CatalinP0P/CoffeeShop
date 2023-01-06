using System;
namespace CoffeeShop.Models
{
	public class CartModel
	{

		public CartModel()
		{

		}

		public string UserId { get; set; }

		public Product Product { get; set; }

		public CartProducts CartProduct { get; set; }

	}
}

