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
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public AdminController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public string GetRole()
        {
            string userRole = "Customer";

             var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if ( claims == null )
            {
                return userRole;
            }

            var role = _context.AccountRoles.FirstOrDefault(m=>m.UserId == claims.Value);

            if ( role == null )
            {
                userRole = "Customer";
                return userRole;
            } 


            if ( role.Role == "Admin" )
                userRole = "Admin";

            return userRole;
        }

        public IActionResult ManageOrders()
        {
            ManageOrdersViewModel viewModel = new ManageOrdersViewModel();
            

            if (GetRole() != "Admin")
                return RedirectToAction("Index", "Home");

            var orders = _context.Orders.ToList();
            viewModel.Orders = orders;
            return View(viewModel);
        }

        public IActionResult FilterOrders(ManageOrdersViewModel vm)
        {
            if ( vm.Filter == "Show All" )
                return RedirectToAction("ManageOrders", "Admin");

            ManageOrdersViewModel viewModel = new ManageOrdersViewModel();
            viewModel.Filter = vm.Filter;
            List<Order> orderList = new List<Order>();

            foreach( Order ord in _context.Orders )
            {
                if ( ord.Status == vm.Filter )
                {
                    orderList.Add(ord);
                }
            }


            if (GetRole() != "Admin")
                return RedirectToAction("Index", "Home");

            var orders = orderList;
            viewModel.Orders = orders;
            return View("ManageOrders", viewModel);
        
        }


        public IActionResult ManageRoles()
        {
            return View();
        }

        public IActionResult OrderStatus(int id )
        {
            var orderInDb = _context.Orders.SingleOrDefault(m => m.Id == id);
            if (orderInDb == null)
                return View("ManageOrders", _context.Orders.ToList());

            var vm = new OrderViewModel
            {
                Order = orderInDb
            };

            return View(vm);

        }


        public IActionResult OrderAdress(int orderid)
        {
            var orderInDb = _context.Orders.SingleOrDefault( m=>m.Id == orderid);
            if (orderInDb == null)
                return View("ManageOrders", _context.Orders.ToList());


            return View(orderInDb);

        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SaveStatus(Order order)
        {
            var orderInDb = _context.Orders.Single(m=>m.Id == order.Id);
            orderInDb.Status = order.Status;
            _context.SaveChanges();
            return View("ManageOrders", _context.Orders.ToList());
            
        }
    }
}

