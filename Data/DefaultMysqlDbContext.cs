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
    public DbSet<VendorAccount> Vendors { get; set; }
    // public DbSet<MyUser> MyUsers { get; set; }
    public DbSet<CustomerCartItem> CustomerCartItems { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<CustomerOrderPerVendor> CustomerOrderPerVendors {get;set;}
    public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    // public DbSet<Role> MyRoles {get;set;}
    // public DbSet<UserRole> MyUserRoles {get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roleAdmin = new IdentityRole { Name = "Admin" };
        roleAdmin.NormalizedName = roleAdmin.Name.ToUpper();

        var roleVendor = new IdentityRole { Name = "Vendor" };
        roleVendor.NormalizedName = roleVendor.Name.ToUpper();

        var roleUser = new IdentityRole { Name = "User" };
        roleUser.NormalizedName = roleUser.Name.ToUpper();

        var roles = new List<IdentityRole>(){
            roleAdmin,
            roleVendor,
            roleUser,
        };

        builder.Entity<IdentityRole>().HasData(roles);

        // 
        var userAdmin = new AppUser
        {
            UserName = "admin",
            Email = "admin@gmail.com"
        };

        var userVendor = new AppUser
        {
            UserName = "vendor1",
            Email = "vendor1@gmail.com"
        };

        var userCustomer = new AppUser
        {
            UserName = "customer1",
            Email = "customer1@gmail.com"
        };

        userAdmin.NormalizedEmail = userAdmin.Email.ToUpper();
        userAdmin.NormalizedUserName = userAdmin.UserName.ToUpper();

        userVendor.NormalizedEmail = userVendor.Email.ToUpper();
        userVendor.NormalizedUserName = userVendor.UserName.ToUpper();

        userCustomer.NormalizedEmail = userCustomer.Email.ToUpper();
        userCustomer.NormalizedUserName = userCustomer.UserName.ToUpper();

        var password = "123Mm,";
        var passwordHasher = new PasswordHasher<AppUser>();

        userAdmin.PasswordHash = passwordHasher.HashPassword(userAdmin, password);
        userVendor.PasswordHash = passwordHasher.HashPassword(userVendor, password);
        userCustomer.PasswordHash = passwordHasher.HashPassword(userCustomer, password);

        var users = new List<AppUser>() { userAdmin, userVendor, userCustomer };

        builder.Entity<AppUser>().HasData(users);

        var mapUserRoles = new List<IdentityUserRole<string>>();
        mapUserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userAdmin.Id,
            RoleId = roleAdmin.Id,
        });
        mapUserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userAdmin.Id,
            RoleId = roleUser.Id,
        });
        mapUserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userCustomer.Id,
            RoleId = roleUser.Id,
        });
        mapUserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userVendor.Id,
            RoleId = roleUser.Id,
        });
        mapUserRoles.Add(new IdentityUserRole<string>
        {
            UserId = userVendor.Id,
            RoleId = roleVendor.Id,
        });

        builder.Entity<IdentityUserRole<string>>().HasData(mapUserRoles);

        var vendor1 = new VendorAccount
        {
            CreatedAt = DateTime.Now,
            Name = "vendor maju",
            PictPath = "",
            UserId = userVendor.Id,
            Id = Guid.NewGuid(),
        };

        builder.Entity<VendorAccount>().HasData(new List<VendorAccount>() { vendor1 });

        var food1 = new Food
        {
            CreatedAt = DateTime.Now,
            FoodPict = "",
            Name = "jus jambu",
            Price = 10000,
            VendorId = vendor1.Id,
            Id = Guid.NewGuid(),
        };

        builder.Entity<Food>().HasData(new List<Food>() { food1 });
    }
}