using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface ICustomerCartRepository
    {
        Task<IEnumerable<CustomerCartItem>> GetAll();
        Task<IEnumerable<CustomerCartItem>> GetAllByUser(AppUser user);
        Task<CustomerCartItem?> GetOneByUserAndFoodId(AppUser user, Guid foodId);
        Task<CustomerCartItem?> GetOneByUserAndId(AppUser user, Guid id);
        Task<CustomerCartItem> Add(CustomerCartItem item);
        Task<CustomerCartItem> Update(CustomerCartItem item);
        Task Delete(CustomerCartItem item);
    }
}