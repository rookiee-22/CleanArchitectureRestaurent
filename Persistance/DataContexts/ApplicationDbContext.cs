using Domain.Entities.BillItems;
using Domain.Entities.Bills;
using Domain.Entities.CartItems;
using Domain.Entities.Carts;
using Domain.Entities.Categories;
using Domain.Entities.Orders;
using Domain.Entities.Otps;
using Domain.Entities.Products;
using Domain.Entities.Tables;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DataContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillItem> BillItems { get; set; }  
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Otp> Otps { get; set; }



}