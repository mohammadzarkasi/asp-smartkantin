using smartkantin.Models;

namespace smartkantin.Repository
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod?> GetOneById(Guid id);
        Task<IEnumerable<PaymentMethod>> GetAll();
        Task<PaymentMethod> Add(PaymentMethod p);
        Task<PaymentMethod> Update(PaymentMethod p);
        Task Delete(PaymentMethod p);

        Task<IEnumerable<PaymentMethod>> GetManyByCode(string code);
    }
}