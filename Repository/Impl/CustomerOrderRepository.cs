using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly DefaultMysqlDbContext dbContext;
        private readonly ICustomerCartRepository customerCartRepository;

        public CustomerOrderRepository(DefaultMysqlDbContext dbContext, ICustomerCartRepository customerCartRepository)
        {
            this.dbContext = dbContext;
            this.customerCartRepository = customerCartRepository;
        }
        public Task<CustomerOrder> Add(CustomerOrder order, IEnumerable<CustomerOrderDetail> details)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerOrder> Add(CustomerOrder order)
        {
            var transaction = dbContext.Database.BeginTransaction();

            await dbContext.AddAsync(order);
            await dbContext.SaveChangesAsync();

            foreach(var ov in order.orderPerVendors)
            {
                foreach(var d in ov.orderDetails)
                {
                    await customerCartRepository.DeleteByFoodIdAndUserId(d.FoodId, order.CustomerId);
                }
            }

            transaction.Commit();

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
            return await GetAllByCustomerId(user.Id);
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllByCustomerId(Guid userId)
        {
            var result = await dbContext
                .CustomerOrders
                .Where(item => item.CustomerId == userId)
                .ToListAsync();
            return result;
        }

        public async Task<CustomerOrder?> GetOneByIdAndCustomer(MyUser user, Guid Id)
        {
            return await GetOneByIdAndCustomerId(Id, user.Id);
        }

        public async Task<CustomerOrder?> GetOneByIdAndCustomerId(Guid id, Guid userId)
        {
            var result = await dbContext.CustomerOrders
                .Where(item => item.Id == id && item.CustomerId == userId)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<CustomerOrder> Update(CustomerOrder order, IEnumerable<CustomerOrderDetail> details)
        {
            throw new NotImplementedException();
        }
    }
}