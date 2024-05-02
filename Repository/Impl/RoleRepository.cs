using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Models;

namespace smartkantin.Repository.Impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DefaultMysqlDbContext dbContext;

        public RoleRepository(DefaultMysqlDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<MyRole>> GetAll()
        {
            return await dbContext.MyRoles.ToListAsync();
        }

        public async Task<MyRole?> GetOneById(Guid id)
        {
            return await dbContext.MyRoles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }
    }
}