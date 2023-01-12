using CoffeeShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Data;

public class ApplicationDbContext : IdentityDbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<CartProducts> CartProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }
}

