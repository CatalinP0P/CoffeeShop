using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Controllers;

namespace CoffeeShop.Controllers.Api
{
    [ApiController]
    [Route("api/{controller}")]
    public class ProductsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {   
            return _context.Products.ToList();
        }

        [HttpPost]
        public void PostProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        [HttpDelete]
        [Route("{id}")]
        public void DeleteProduct(int id)
        {
            var productInDb = _context.Products.Single(m=>m.Id == id);
            _context.Products.Remove(productInDb);
            _context.SaveChanges();
        }

    }
}
