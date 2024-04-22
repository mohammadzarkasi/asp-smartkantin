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
    public async Task<VendorAccount> Add(VendorAccount item)
    {
        await dbContext.AddAsync(item);
        await this.Save();
        return item;
        // throw new NotImplementedException();
    }

    public async Task Delete(VendorAccount item)
    {
        // throw new NotImplementedException();
        dbContext.Remove(item);
        await this.Save();
    }

    public async Task<IEnumerable<VendorAccount>> GetAll()
    {
        // throw new NotImplementedException();
        var result = await dbContext.Vendors.ToListAsync();
        return result;
    }

    public async Task<VendorAccount?> GetById(Guid id)
    {
        // throw new NotImplementedException();
        var result = await dbContext.Vendors.FirstOrDefaultAsync((item) => item.Id == id);
        return result;
    }

    public async Task<VendorAccount?> GetByUserId(string UserId)
    {
        var result = await dbContext.Vendors.Where(v => v.UserId == UserId).FirstOrDefaultAsync();
        return result;
    }

    public async Task Save()
    {
        // throw new NotImplementedException();
        // dbContext.SaveChanges();
        await dbContext.SaveChangesAsync();
    }

    public async Task<VendorAccount> Update(VendorAccount item)
    {
        // throw new NotImplementedException();
        dbContext.Update(item);
        await Save();
        return item;
    }
}