using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl;
public class FoodRepository : IFoodRepository
{
    private readonly DefaultMysqlDbContext dbContext;

    public FoodRepository(DefaultMysqlDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Food> Add(Food item)
    {
        await dbContext.AddAsync(item);
        await Save();
        return item;
    }

    public async Task FlagDelete(Food item)
    {
        // dbContext.Remove(item);
        item.DeletedAt = DateTime.Now;

        dbContext.Update(item);

        await Save();
    }

    public async Task<IEnumerable<Food>> GetAll()
    {
        var result = await dbContext.Foods.Where(f => f.DeletedAt == null).ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Food>> GetAllByVendor(Guid VendorId)
    {
        var result = await dbContext.Foods.Where(f => f.VendorId == VendorId && f.DeletedAt == null).ToListAsync();
        return result;
    }

    public async Task<Food?> GetById(Guid id)
    {
        var result = await dbContext.Foods.FirstOrDefaultAsync((item) => item.Id == id && item.DeletedAt == null);
        return result;
    }

    public async Task<Food?> GetByIdAndVendorId(Guid id, Guid VendorId)
    {
        var result = await dbContext.Foods.FirstOrDefaultAsync((item) => item.Id == id && item.DeletedAt == null && item.VendorId == VendorId);
        return result;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<Food> Update(Food item)
    {
        dbContext.Update(item);
        await Save();
        return item;
    }
}