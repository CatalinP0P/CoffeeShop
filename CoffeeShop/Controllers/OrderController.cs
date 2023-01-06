using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Data;
using CoffeeShop.Models;
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
            var adress = new Adress
            {
                UserId = id
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

            foreach ( var product in _context.CartProducts )
            {
                if ( product.UserId == adress.UserId )
                {
                    productids += $"{product.ProductId}#";
                }
            }


            var Order = new Order
            {
                UserId = adress.UserId,

                City = adress.City,
                Country = adress.Country,
                PhoneNumber = adress.PhoneNumber,
                Street = adress.Street,
                Number = adress.Number,

                ProductIds = productids,
                OrderDate = DateTime.Now

            };

            _context.Orders.Add(Order);
            _context.SaveChanges();

            var orderInDb = _context.Orders.Single(m => m.OrderDate == Order.OrderDate && m.UserId == Order.UserId);

            return View("Message",orderInDb);

        }


    }
}

