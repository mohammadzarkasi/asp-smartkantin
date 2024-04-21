using smartkantin.Models;

namespace smartkantin.Repository;
public interface IFoodRepository
{
    Task<IEnumerable<Food>> GetAll();
    Task<Food?> GetById(Guid id);
    Task<Food> Add(Food item);
    Task<Food> Update(Food item);
    Task Delete(Food item);
    Task Save();
}