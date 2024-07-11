using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Snack> Snacks { get; set; } = null!;
        public DbSet<CartPurchaseItem> CartPurchaseItens { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<DetailOrder> DetailsOrder { get; set; } = null!;

    }
}
