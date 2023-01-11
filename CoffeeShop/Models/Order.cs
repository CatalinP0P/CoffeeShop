using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
	public class Order
	{
		public Order()
		{
            
		}

		public int Id { get; set; }

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


        public int Price { get; set; }

        public DateTime OrderDate { get; set; }

		public string ProductIds { get; set; } // with string format ex : 1#13#21#102 separated by #

        public string Status { get; set; }

	}
}

