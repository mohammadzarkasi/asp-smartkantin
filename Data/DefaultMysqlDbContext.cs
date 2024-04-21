using Microsoft.EntityFrameworkCore;
using smartkantin.Models;

namespace smartkantin.Data;

public class DefaultMysqlDbContext : DbContext
{
    public DefaultMysqlDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<MyUser> MyUsers { get; set; }
    public DbSet<CustomerCartItem> CustomerCartItems {get;set;}
    public DbSet<CustomerOrder> CustomerOrders {get;set;}
    public DbSet<CustomerOrderDetail> CustomerOrderDetails {get;set;}
    public DbSet<PaymentMethod> PaymentMethods {get;set;}
    public DbSet<Role> Roles {get;set;}
    public DbSet<UserRole> UserRoles {get;set;}
}