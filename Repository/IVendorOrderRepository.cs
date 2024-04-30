using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface IVendorOrderRepository
    {
        Task<CustomerOrder?> GetById(Guid id);
        Task<IEnumerable<CustomerOrder>> GetAll();
        Task<IEnumerable<CustomerOrderPerVendor>> GetAllByVendor(VendorAccount vendor);
        Task<CustomerOrder?> Decline(CustomerOrder order);
        Task<CustomerOrder?> Accept(CustomerOrder order);
        Task<CustomerOrder?> NotifyOrderDone(CustomerOrder order);
        Task<CustomerOrder?> MarkAsDone(CustomerOrder order);
    }
}