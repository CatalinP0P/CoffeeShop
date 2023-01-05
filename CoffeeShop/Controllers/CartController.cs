using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoffeeShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: /<controller>/

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public CartController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [Authorize]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claims == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<CartViewModel> Products = new List<CartViewModel>();

            foreach( var p in _context.CartProducts )
            {
                if ( p.UserId == claims.Value )
                {
                    var productInDb = _context.Products.Single(m => m.Id == p.ProductId);

                    var ViewModel = new CartViewModel
                    {
                        CartProduct = p,
                        Product = productInDb
                    };

                    Products.Add(ViewModel);
                }
            }

            return View("Index", Products);
        }


        public IActionResult Delete( int id )
        {
            var cartProductInDb = _context.CartProducts.SingleOrDefault(m => m.Id == id);

            if (cartProductInDb == null)
                return RedirectToAction("Index", "Cart");

            _context.CartProducts.Remove(cartProductInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Cart");
                   
        }


    }
}

