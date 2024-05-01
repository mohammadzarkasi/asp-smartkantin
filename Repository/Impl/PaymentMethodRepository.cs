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
        public async Task<PaymentMethod> Add(PaymentMethod p)
        {

            p.CreatedOn = DateTime.Now.ToUniversalTime();

            await dbContext.AddAsync(p);
            await dbContext.SaveChangesAsync();

            return p;
        }

        public async Task Delete(PaymentMethod p)
        {
            // dbContext.Remove(p);
            p.DeletedOn = DateTime.Now.ToUniversalTime();

            dbContext.Update(p);

            await dbContext.SaveChangesAsync();
        }

        // public async Task Delete(Guid id)
        // {
        //     var item = await dbContext.PaymentMethods.FindAsync(id);
        //     if(item != null)
        //     {
        //         await Delete(item);
        //     }
        // }

        public async Task<IEnumerable<PaymentMethod>> GetAll()
        {
            return await dbContext
                .PaymentMethods
                .Where(p => p.DeletedOn == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentMethod>> GetManyByCode(string code)
        {
            return await dbContext
                .PaymentMethods
                .Where(p => p.Code == code && p.DeletedOn == null)
                .ToListAsync();
        }

        public async Task<PaymentMethod?> GetOneById(Guid id)
        {
            return await dbContext
                .PaymentMethods
                .Where(p => p.DeletedOn == null && p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<PaymentMethod> Update(PaymentMethod p)
        {
            p.UpdatedOn = DateTime.Now.ToUniversalTime();

            dbContext.Update(p);
            await dbContext.SaveChangesAsync();

            return p;
        }
    }
}