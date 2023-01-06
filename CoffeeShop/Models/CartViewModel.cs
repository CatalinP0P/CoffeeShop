using System;
namespace CoffeeShop.Models
{
	public class CartViewModel
	{
		public CartViewModel()
		{
		}

		public string UserId { get; set; }

		public List<CartModel> CartModels {get;set;}



	}
}

