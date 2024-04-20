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
}