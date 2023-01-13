namespace CoffeeShop.Models
{
    public class ProductsIndexViewModel
    {
        public ProductsIndexViewModel()
        {

        }

        public List<Product> Products { get; set; }
        public string UserRole { get; set; }

        public string Filter { get; set; }

    }
}