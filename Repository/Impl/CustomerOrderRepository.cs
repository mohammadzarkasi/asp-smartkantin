using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly DefaultMysqlDbContext dbContext;

        public CustomerOrderRepository(DefaultMysqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<CustomerOrder> Add(CustomerOrder order, IEnumerable<CustomerOrderDetail> details)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerOrder> Add(CustomerOrder order)
        {
            await dbContext.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public Task Delete(CustomerOrder order)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerOrder>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllByCustomer(MyUser user)
        {
            var result = await dbContext.CustomerOrders.Where(item => item.CustomerId == user.Id).ToListAsync();
            return result;
        }

        public async Task<CustomerOrder?> GetOneByIdAndCustomer(MyUser user, Guid Id)
        {
            var result = await dbContext.CustomerOrders
                .Where(item => item.Id == Id && item.CustomerId == user.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<CustomerOrder> Update(CustomerOrder order, IEnumerable<CustomerOrderDetail> details)
        {
            throw new NotImplementedException();
        }
    }
}