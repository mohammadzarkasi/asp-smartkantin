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

        public async Task<IEnumerable<CustomerCartItem>> GetAllByUser(AppUser user)
        {
            var result = await dbContext.CustomerCartItems.Where(item => item.UserId == user.Id).ToListAsync();
            return result;
        }

        public async Task<CustomerCartItem?> GetOneByUserAndFoodId(AppUser user, Guid foodId)
        {
            var result = await dbContext.CustomerCartItems.Where(item => item.UserId == user.Id && item.FoodId == foodId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<CustomerCartItem?> GetOneByUserAndId(AppUser user, Guid id)
        {
            var result = await dbContext.CustomerCartItems.Where(item => item.UserId == user.Id && item.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<CustomerCartItem> Update(CustomerCartItem item)
        {
            item.UpdatedOn = DateTime.Now;

            dbContext.Update(item);

            await dbContext.SaveChangesAsync();

            return item;
        }
    }
}