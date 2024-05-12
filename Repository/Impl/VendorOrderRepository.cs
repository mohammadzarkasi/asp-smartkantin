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

        public async Task<IEnumerable<CustomerOrderPerVendor>> GetAllByVendor(VendorAccount vendor)
        {
            // throw new NotImplementedException();
            // return await dbContext
            //     .CustomerOrders
            //     .Include(ord => ord.orderPerVendors)
            //         .ThenInclude(ordv => ordv.Select(v => v.orderDetails))
            //     .Where(ord => ord.orderPerVendors.Any(ordv => ordv.VendorId==vendor.Id))
            //     .ToListAsync();
            
            return await dbContext
                .CustomerOrderPerVendors
                .Include(ord => ord.Order)
                    .ThenInclude(ord => ord.Customer)
                .Where(ord => ord.VendorId == vendor.Id)
                .ToListAsync();
        }

        public Task<CustomerOrder?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerOrderPerVendor?> GetOneByIdAndVendorId(Guid orderId, Guid vendorId)
        {
            Console.WriteLine("get detail order, id order: " + orderId + ", vendor: " + vendorId);
            return await dbContext
                .CustomerOrderPerVendors
                .Include(ordVendor => ordVendor.Order)
                    .ThenInclude(ord => ord.Customer)
                .Include(ordVendor => ordVendor.orderDetails)
                .Where(ordVendor => ordVendor.VendorId == vendorId 
                        && ordVendor.Order.Id == orderId)
                .FirstOrDefaultAsync();
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