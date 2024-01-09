using Microsoft.EntityFrameworkCore;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.OrderAPI.Models;

namespace Mango.Services.OrderAPI.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
    {
    }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

}
