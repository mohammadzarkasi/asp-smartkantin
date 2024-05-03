using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface ICustomerCartRepository
    {
        Task<IEnumerable<CustomerCartItem>> GetAll();
        Task<IEnumerable<CustomerCartItem>> GetAllByUser(MyUser user);
        Task<IEnumerable<CustomerCartItem>> GetAllByUserId(Guid id);
        Task<CustomerCartItem?> GetOneByFoodIdAndUser( Guid foodId, MyUser user);
        Task<CustomerCartItem?> GetOneByFoodIdAndUserId(Guid foodId, Guid userId);
        Task<CustomerCartItem?> GetOneByIdAndUser(Guid id, MyUser user);
        Task<CustomerCartItem?> GetOneByIdAndUserId(Guid id, Guid userId);
        Task<CustomerCartItem> Add(CustomerCartItem item);
        Task<CustomerCartItem> Update(CustomerCartItem item);
        Task Delete(CustomerCartItem item);
    }
}