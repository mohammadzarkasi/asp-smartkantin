using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class CustomerCartRepository : ICustomerCartRepository
    {
        private readonly DefaultMysqlDbContext dbContext;

        public CustomerCartRepository(DefaultMysqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CustomerCartItem> Add(CustomerCartItem item)
        {
            item.CreatedOn = DateTime.Now;
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task Delete(CustomerCartItem item)
        {
            dbContext.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<CustomerCartItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerCartItem>> GetAllByUser(MyUser user)
        {
            return await GetAllByUserId(user.Id);
        }

        public async Task<IEnumerable<CustomerCartItem>> GetAllByUserId(Guid id)
        {
            var result = await dbContext.CustomerCartItems
                .Include(c => c.TheFood)
                    .ThenInclude(f => f.Vendor)
                .Where(item => item.UserId == id)
                .ToListAsync();
            return result;
        }

        public async Task<CustomerCartItem?> GetOneByFoodIdAndUser(Guid foodId, MyUser user)
        {
            return await GetOneByFoodIdAndUserId(foodId, user.Id);
        }

        public async Task<CustomerCartItem?> GetOneByIdAndUser(Guid id, MyUser user)
        {
            return await GetOneByIdAndUserId(id, user.Id);
        }

        public async Task<CustomerCartItem> Update(CustomerCartItem item)
        {
            item.UpdatedOn = DateTime.Now;

            dbContext.Update(item);

            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<CustomerCartItem?> GetOneByFoodIdAndUserId(Guid foodId, Guid userId)
        {
            Console.WriteLine("food id: " + foodId + ", " + userId);
            var result = await dbContext
                .CustomerCartItems
                .Where(item => item.UserId == userId && item.FoodId == foodId)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<CustomerCartItem?> GetOneByIdAndUserId(Guid id, Guid userId)
        {
            var result = await dbContext
                .CustomerCartItems
                .Where(item => item.UserId == userId && item.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task DeleteByFoodIdAndUserId(Guid foodId, Guid userId)
        {
            var target = await dbContext
                .CustomerCartItems
                .Where(c => c.FoodId == foodId && c.UserId == userId)
                .FirstOrDefaultAsync();
            
            if(target == null)
            {
                return;
            }

            dbContext.Remove(target);
            await dbContext.SaveChangesAsync();
        }
    }
}