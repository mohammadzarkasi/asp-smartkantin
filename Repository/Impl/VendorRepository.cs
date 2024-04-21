using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl;

public class VendorRepository : IVendorRepository
{
    private readonly DefaultMysqlDbContext dbContext;

    public VendorRepository(DefaultMysqlDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Vendor> Add(Vendor item)
    {
        await dbContext.AddAsync(item);
        await this.Save();
        return item;
        // throw new NotImplementedException();
    }

    public async Task Delete(Vendor item)
    {
        // throw new NotImplementedException();
        dbContext.Remove(item);
        await this.Save();
    }

    public async Task<IEnumerable<Vendor>> GetAll()
    {
        // throw new NotImplementedException();
        var result = await dbContext.Vendors.ToListAsync();
        return result;
    }

    public async Task<Vendor?> GetById(Guid id)
    {
        // throw new NotImplementedException();
        var result = await dbContext.Vendors.FirstOrDefaultAsync((item) => item.Id == id);
        return result;
    }

    public async Task Save()
    {
        // throw new NotImplementedException();
        // dbContext.SaveChanges();
        await dbContext.SaveChangesAsync();
    }

    public async Task<Vendor> Update(Vendor item)
    {
        // throw new NotImplementedException();
        dbContext.Update(item);
        await Save();
        return item;
    }
}