using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly DefaultMysqlDbContext dbContext;

        public PaymentMethodRepository(DefaultMysqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<PaymentMethod> Add(PaymentMethod p)
        {
            throw new NotImplementedException();
        }

        public Task Delete(PaymentMethod p)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await dbContext
                .PaymentMethods
                .ToListAsync();
        }

        public Task<PaymentMethod> GetOneById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentMethod> Update(PaymentMethod p)
        {
            throw new NotImplementedException();
        }
    }
}