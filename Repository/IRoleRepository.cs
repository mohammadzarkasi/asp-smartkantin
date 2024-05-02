using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface IRoleRepository
    {
        Task<MyRole?> GetOneById(Guid id);
        Task<IEnumerable<MyRole>> GetAll();
    }
}