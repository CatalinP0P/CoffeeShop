using Newtonsoft.Json;

namespace CoffeeShop.Models
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        void PostProduct(Product product);
        void DeleteProduct(int id);

    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync("http://localhost:5130/api/productsapi");
            List<Product> products = new List<Product>();
            var body = await response.Content.ReadAsStringAsync();
            products =  JsonConvert.DeserializeObject<List<Product>>(body);
            
            return products;
        }


        public async Task<Product> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync("http://localhost:5130/api/productsapi");
            List<Product> products = new List<Product>();
            var body = await response.Content.ReadAsStringAsync();
            products =  JsonConvert.DeserializeObject<List<Product>>(body);
            
            var product = products.SingleOrDefault( m=>m.Id == id );
            return product;
        }

        public async void PostProduct(Product product)
        {
            var uri = "http://localhost:5130/api/productsapi";
            var newProduct = JsonConvert.SerializeObject(product);
            var payload = new StringContent(newProduct, System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(uri, payload);
        }

        public async void DeleteProduct( int id )
        {
            var uri = "http://localhost:5130/api/productsapi" + "/" + id.ToString();
            var result =  await _httpClient.DeleteAsync(uri);
        }
    }
}