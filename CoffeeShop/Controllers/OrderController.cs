using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeShop.Controllers
{
    public class OrderController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public OrderController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult AdressForm(string id) // userid
        {
            var cartprod = _context.CartProducts.FirstOrDefault(m=>m.UserId == id);
            if (cartprod == null)
                return RedirectToAction("Index","Cart");

            var adress = new Adress
            {
                UserId = id,
                Country = "Romania"
            };

            return View(adress);
        }

        public IActionResult SendOrder( Adress adress )
        {
            if ( !ModelState.IsValid )
            {
                return View("AdressForm", adress);
            }

            


            string productids = "#";

            int totalPrice = 0;

            foreach ( var product in _context.CartProducts )
            {
                if ( product.UserId == adress.UserId )
                {
                    productids += $"{product.ProductId}#";
                    var productInDb = _context.Products.Single(m=>m.Id == product.ProductId);
                    totalPrice += productInDb.Price;
                }
            }

            // Order price




            var Order = new Order
            {
                UserId = adress.UserId,

                City = adress.City,
                Country = adress.Country,
                PhoneNumber = adress.PhoneNumber,
                Street = adress.Street,
                Number = adress.Number,

                Price = totalPrice,

                ProductIds = productids,
                OrderDate = DateTime.Now

            };

            _context.Orders.Add(Order);
            _context.SaveChanges();

            foreach( CartProducts prod in _context.CartProducts )
            {
                if ( prod.UserId == adress.UserId )
                {
                    _context.CartProducts.Remove(prod);
                }

            }

            _context.SaveChanges();


            var orderInDb = _context.Orders.Single(m => m.OrderDate == Order.OrderDate && m.UserId == Order.UserId);

            return View("Message",orderInDb);

        }

        [Authorize]
        public IActionResult ShowOrders()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userid = claims.Value;


            List<ShowOrdersViewModel> orders = new List<ShowOrdersViewModel>();

            foreach ( Order order in _context.Orders )
            {
                List<int> idList = new List<int>();

                string[] ids = order.ProductIds.Split('#');

                for ( int i = 1; i < ids.Length-1; i++ )
                {
                    ids[i] = ids[i].Replace('"', ' ');
                    idList.Add(Int32.Parse(ids[i]));
                }

                List<string> imageUrls = new List<string>();
                List<Product> productList = new List<Product>();

                foreach ( int id in idList )
                {
                    var productInDb = _context.Products.Single(m => m.Id == id);

                    imageUrls.Add(productInDb.ImageURL);

                    productList.Add(productInDb);

                }


                ShowOrdersViewModel temp = new ShowOrdersViewModel
                {
                    Orders = order,
                    ProductIds = idList,
                    ProductImageUrl = imageUrls,
                    Products = productList
                };

                orders.Add(temp);
            }

            return View(orders);


        }

    }
}

