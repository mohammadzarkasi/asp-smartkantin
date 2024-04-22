using smartkantin.Models;

namespace smartkantin.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<VendorAccount>> GetAll();
    Task<VendorAccount?> GetById(Guid id);
    Task<VendorAccount?> GetByUserId(string UserId);
    Task<VendorAccount> Add(VendorAccount item);
    Task<VendorAccount> Update(VendorAccount item);
    Task Delete(VendorAccount item);
    Task Save();
}