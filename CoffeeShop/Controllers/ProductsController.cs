using CoffeeShop.Data;
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
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeShop.Controllers
{

    public class ProductsController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public ProductsController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            IProductService productService)
        {
            _logger = logger;
            _context = context;
            _productService = productService;
        }

        // public IActionResult TestAPI()
        // {
        //     var product = new Product()
        //     {
        //         Name = "TestAPI",
        //         Category = "TestAPI",
        //         Stock = 100,
        //         Price = 100,
        //         ImageURL = "test"
        //     };

        //     _productService.PostProduct(product);

        //     return RedirectToAction("Index", "Products");
        // }


        // Products
        public async Task<IActionResult> Index()
        {
            string userRole = GetRole();

            ProductsIndexViewModel viewModel = new ProductsIndexViewModel
            {
                Products = await _productService.GetProducts(),
                UserRole = userRole
            };

            return View(viewModel);
        }

       public IActionResult Filter(string Filter)
        {
            string userRole = GetRole();

            var productList = new List<Product>();

            foreach ( var product in _context.Products )
            {
                {
                    productList.Add(product);
                }
            }

            ProductsIndexViewModel viewModel = new ProductsIndexViewModel
            {
                Products = productList,
                UserRole = userRole
            };

            return View("Index",viewModel);
        }


        // Products/New
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


        // Products/Details/{id}
        public IActionResult Details(int id)
        {
            var productInDb = _context.Products.SingleOrDefault(m => m.Id == id);

            if (productInDb == null)
                return RedirectToAction("Index", "Products");

            return View(productInDb);
        }





        // ACTIONS ===============================


        // Save Method Called By the ProductForm
        [ValidateAntiForgeryToken]
        public IActionResult Save(Product product)
        {
            if (!ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    var vm = new ProductViewModel
                    {
                        Product = product
                    };

                    return View("ProductForm", vm);

                }

            }

            if (product.Id == 0)
            {
                _productService.PostProduct(product);
            }
            else
            {
                var productInDb = _context.Products.Single(m => m.Id == product.Id);
                productInDb.Name = product.Name;
                productInDb.Stock = product.Stock;
                productInDb.Category = product.Category;
                productInDb.Price = product.Price;
                productInDb.ImageURL = product.ImageURL;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Products");
        }


        public async Task<IActionResult> Delete(int id)
        {
            List<Product> productsList = await _productService.GetProducts();
            var productInDb = productsList.SingleOrDefault(m=>m.Id == id);
            if (productInDb != null)
            {
                _productService.DeleteProduct(id);
            }

            return RedirectToAction("Index", "Products");
        }


        public IActionResult AddToCart(int id)
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




        public string GetRole()
        {
            string userRole = "Customer";

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claims == null)
            {
                return userRole;
            }

            var role = _context.AccountRoles.FirstOrDefault(m => m.UserId == claims.Value);

            if (role == null)
            {
                userRole = "Customer";
                return userRole;
            }


            if (role.Role == "Admin")
                userRole = "Admin";

            return userRole;
        }
    }
}

