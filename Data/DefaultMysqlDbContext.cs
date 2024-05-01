using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smartkantin.Models;
using smartkantin.Tools;

namespace smartkantin.Data;

// public class DefaultMysqlDbContext : IdentityDbContext<AppUser>
public class DefaultMysqlDbContext : DbContext
{
    public DefaultMysqlDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Food> Foods { get; set; }
    public DbSet<VendorAccount> Vendors { get; set; }
    public DbSet<MyUser> MyUsers { get; set; }
    public DbSet<CustomerCartItem> CustomerCartItems { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<CustomerOrderPerVendor> CustomerOrderPerVendors { get; set; }
    public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<MyRole> MyRoles { get; set; }
    public DbSet<MyUserRole> MyUserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roleAdmin = new MyRole
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Id = Guid.Parse("b2a7f527-7140-46bc-84cd-2bf84ec319ac"),
            Name = "Admin",
        };
        var roleVendor = new MyRole
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Id = Guid.Parse("529a7e61-eb02-409f-a8d2-66f6e5f7289b"),
            Name = "Vendor",
        };
        var roleCustomer = new MyRole
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Id = Guid.Parse("f69f7dff-72bd-49aa-be27-b70da6f7d36d"),
            Name = "Customer",
        };

        builder.Entity<MyRole>().HasData([roleAdmin, roleVendor, roleCustomer]);

        var userAdmin = new MyUser
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Email = "admin@gmail.com",
            Id = Guid.Parse("6f3550e5-4d1d-4eec-b585-bc9cfa026761"),
            Status = "active",
            Username = "admin",
            Password = "$2a$13$zMipZU75HAPSerhKKyaUBOvGTx9AQ/mkbdjnckW5MGWuqU3nVJDM.", // '123'
        };
        var userVendor = new MyUser
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Email = "vendor1@gmail.com",
            Id = Guid.Parse("10aa4fe9-316f-4425-afa8-43c0b59dca97"),
            Status = "active",
            Username = "vendor1",
            Password = "$2a$13$T.FB4hnbA09mtqrUF9.jkeGy5dwDDBkGjx.wftAIzY2QcigqWvLW2", // '123'
        };
        var userCustomer = new MyUser
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Email = "customer1@gmail.com",
            Id = Guid.Parse("84951dc1-5a5f-44fe-b963-b23b005c0f1a"),
            Status = "active",
            Username = "customer1",
            Password = "$2a$13$op/kspb7XI5lxjYJ4mmFbuTkluC7JcTn7QWshO5ymvBuig2Loz5Dy", // '123'
        };

        builder.Entity<MyUser>().HasData([userAdmin, userVendor, userCustomer]);

        var mapUserRoles = new List<MyUserRole>
        {
            new() {
                CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
                Id = Guid.Parse("f8912f53-3e05-461d-bbf4-9d9df6a83df6"),
                RoleId = roleAdmin.Id,
                UserId = userAdmin.Id,
            },
            new() {
                CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
                Id = Guid.Parse("0d354121-dcda-41cb-87b0-b5d1d444f6db"),
                RoleId = roleVendor.Id,
                UserId = userVendor.Id,
            },
            new() {
                CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
                Id = Guid.Parse("02eea5dd-763e-4e3f-82dd-d9d47b9e184e"),
                RoleId = roleCustomer.Id,
                UserId = userCustomer.Id,
            }
        };
        builder.Entity<MyUserRole>().HasData(mapUserRoles);

        var vendorAcc1 = new VendorAccount
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Id = Guid.Parse("1c728b5d-c153-41bf-8aab-caee14d5db95"),
            Name = "Vendor Satu",
            UserId = userVendor.Id,
        };

        builder.Entity<VendorAccount>().HasData([vendorAcc1]);

        var food1 = new Food
        {
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Id = Guid.Parse("6c7febdc-e16d-4436-9d3c-979b2e7e6437"),
            Name = "jus jambu",
            Price = 10000,
            VendorId = vendorAcc1.Id,
        };

        builder.Entity<Food>().HasData([food1]);

        // seeder payment method
        var payMtd1 = new PaymentMethod
        {
            Id = GuidHelper.ParseGuid("fb88ca7b-502f-444c-8897-3fd3cb7c1771"),
            Code = "cash",
            CreatedOn = new DateTime(2024, 4, 30, 22, 0, 0),
            Name = "Bayar Cash",
            NeedConfirmation = 1,
            NeedUploadPayment = 0,
        };

        builder.Entity<PaymentMethod>().HasData([payMtd1]);
    }
}