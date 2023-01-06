using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
	public class Adress
	{
		public Adress()
		{
		}
		[Required]
		public string UserId { get; set; }

		[Required]
        public string Country { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string Street { get; set; }

		[Required]
		public string Number { get; set; }

		[Required]
		public int PhoneNumber { get; set; }

	}
}

