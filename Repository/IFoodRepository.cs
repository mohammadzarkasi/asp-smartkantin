using smartkantin.Models;

namespace smartkantin.Repository;
public interface IFoodRepository
{
    Task<IEnumerable<Food>> GetAll();
    Task<IEnumerable<Food>> GetAllByVendor(Guid VendorId);
    Task<Food?> GetById(Guid id);
    Task<Food?> GetByIdAndVendorId(Guid id, Guid VendorId);
    Task<Food> Add(Food item);
    Task<Food> Update(Food item);
    Task FlagDelete(Food item);
    Task Save();
}