using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface ICustomerCartRepository
    {
        Task<IEnumerable<CustomerCartItem>> GetAll();
        Task<IEnumerable<CustomerCartItem>> GetAllByUser(MyUser user);
        Task<CustomerCartItem?> GetOneByUserAndFoodId(MyUser user, Guid foodId);
        Task<CustomerCartItem?> GetOneByUserAndId(MyUser user, Guid id);
        Task<CustomerCartItem> Add(CustomerCartItem item);
        Task<CustomerCartItem> Update(CustomerCartItem item);
        Task Delete(CustomerCartItem item);
    }
}