using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class VendorOrderRepository : IVendorOrderRepository
    {
        private readonly DefaultMysqlDbContext dbContext;

        public VendorOrderRepository(DefaultMysqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<CustomerOrder?> Accept(CustomerOrder order)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerOrder?> Decline(CustomerOrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerOrder>> GetAll()
        {
            var result = await dbContext.CustomerOrders.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CustomerOrder>> GetAllByVendor(VendorAccount vendor)
        {
            var result = await dbContext.CustomerOrders.Where(item => item.VendorId == vendor.Id).ToListAsync();
            return result;
        }

        public Task<CustomerOrder?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerOrder?> MarkAsDone(CustomerOrder order)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerOrder?> NotifyOrderDone(CustomerOrder order)
        {
            throw new NotImplementedException();
        }
    }
}