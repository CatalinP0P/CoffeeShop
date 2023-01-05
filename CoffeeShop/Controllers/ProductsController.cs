﻿using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeShop.Controllers
{
    
    public class ProductsController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
     
        public ProductsController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Products
        public IActionResult Index()
        {
            var productList = _context.Products.ToList();
            return View(productList);
        }

        // Products/New
        [Authorize(Roles = "Admin")]
        public IActionResult New()
        {
            var product = new Product();
            var vm = new ProductViewModel
            {
                Product = product
            };

            return View("ProductForm", vm);
        }



        // Products/Edit/{id}
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(m => m.Id == id);
            if (productInDb == null)
                return RedirectToAction("Index", "Products");

            var vm = new ProductViewModel
            {
                Product = productInDb
            };

            return View("ProductForm", vm);
        }




        // ACTIONS ===============================


        // Save Method Called By the ProductForm
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var productInDb = _context.Products.Single(m => m.Id == product.Id);
                productInDb.Name = product.Name;
                productInDb.Stock = product.Stock;
                productInDb.Category = product.Category;
                productInDb.Price = product.Price;
                productInDb.ImageURL = product.ImageURL;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(m => m.Id == id);
            if (productInDb != null)
            {
                _context.Products.Remove(productInDb);
                _context.SaveChanges();
            }


            return RedirectToAction("Index", "Products");

        }


        public IActionResult AddToCart( int id )
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claims == null)
                return RedirectToAction("Index", "Cart");

            string userId = claims.Value;


            var product = new CartProducts
            {
                ProductId = id,
                UserId = userId,
            };

            _context.CartProducts.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

    }
}

