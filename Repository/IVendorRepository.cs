using smartkantin.Models;

namespace smartkantin.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorAccount>> GetAll();
    Task<VendorAccount?> GetById(Guid id);
    Task<VendorAccount?> GetByUser(MyUser user);
    Task<VendorAccount> Add(VendorAccount item);
    Task<VendorAccount> Update(VendorAccount item);
    Task Delete(VendorAccount item);
    Task Save();
}