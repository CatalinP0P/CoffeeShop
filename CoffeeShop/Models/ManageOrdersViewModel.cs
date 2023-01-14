namespace CoffeeShop.Models
{
    public class ManageOrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public string Filter { get; set; }

        public List<string> StatusTypes = new List<string>{
            "Show All",
            StatusType.Canceled.ToString(),
            StatusType.Confirmed.ToString(),
            StatusType.Pending.ToString(),
            StatusType.Received.ToString(),
            StatusType.Returned.ToString(),
            StatusType.Send.ToString()

        };
    }
}