using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface ICustomerOrderRepository
    {
        Task<IEnumerable<CustomerOrder>> GetAll();
        Task<IEnumerable<CustomerOrder>> GetAllByCustomer(AppUser user);
        Task<CustomerOrder?> GetOneByIdAndCustomer(AppUser user, Guid Id);
        Task<CustomerOrder> Add(CustomerOrder order);
        Task<CustomerOrder> Add(CustomerOrder order, IEnumerable<CustomerOrderDetail> details);
        Task<CustomerOrder> Update(CustomerOrder order, IEnumerable<CustomerOrderDetail> details);
        Task Delete(CustomerOrder order);
    }
}