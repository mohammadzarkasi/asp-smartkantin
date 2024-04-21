using smartkantin.Models;

namespace smartkantin.Repository;

public interface IVendorRepository
{
    Task<IEnumerable<Vendor>> GetAll();
    Task<Vendor?> GetById(Guid id);
    Task<Vendor> Add(Vendor item);
    Task<Vendor> Update(Vendor item);
    Task Delete(Vendor item);
    Task Save();
}