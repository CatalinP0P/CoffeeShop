using CoffeeShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data;

public class ApplicationDbContext : IdentityDbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<CartProducts> CartProducts { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

