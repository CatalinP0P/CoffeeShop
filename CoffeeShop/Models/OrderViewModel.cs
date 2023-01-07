using System;
namespace CoffeeShop.Models
{
	public class OrderViewModel
	{
		public OrderViewModel()
		{
			StatusTypes = new List<string>
			{
				StatusType.Canceled.ToString(),
				StatusType.Confirmed.ToString(),
				StatusType.Pending.ToString(),
				StatusType.Received.ToString(),
				StatusType.Send.ToString(),
				StatusType.Returned.ToString()
			};
		}

		public Order Order { get; set; }
		public List<string> StatusTypes { get; set; }
	}
}

