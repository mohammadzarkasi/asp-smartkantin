using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smartkantin.Models;

namespace smartkantin.Data;

public class DefaultMysqlDbContext : IdentityDbContext<AppUser>
{
    public DefaultMysqlDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    // public DbSet<MyUser> MyUsers { get; set; }
    public DbSet<CustomerCartItem> CustomerCartItems {get;set;}
    public DbSet<CustomerOrder> CustomerOrders {get;set;}
    public DbSet<CustomerOrderDetail> CustomerOrderDetails {get;set;}
    public DbSet<PaymentMethod> PaymentMethods {get;set;}
    // public DbSet<Role> MyRoles {get;set;}
    // public DbSet<UserRole> MyUserRoles {get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roles = new List<IdentityRole>(){
            new IdentityRole{
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole{
                Name = "Vendor",
                NormalizedName = "VENDOR"
            },
            new IdentityRole{
                Name = "User",
                NormalizedName = "USER"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}