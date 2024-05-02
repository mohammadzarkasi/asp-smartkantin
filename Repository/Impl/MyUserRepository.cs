using Microsoft.EntityFrameworkCore;
using smartkantin.Data;
using smartkantin.Dto;
using smartkantin.Models;

namespace smartkantin.Repository.Impl;
public class MyUserRepository : IMyUserRepository
{
    private readonly DefaultMysqlDbContext dbContext;

    public MyUserRepository(DefaultMysqlDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<MyUser?> AssignUserToRole(MyUser user, MyRole role)
    {
        var n = new MyUserRole
        {
            RoleId = role.Id,
            UserId = user.Id,
            CreatedOn = DateTime.Now.ToUniversalTime(),
        };
        await dbContext.AddAsync(n);
        await dbContext.SaveChangesAsync();

        var r = await GetOneById(user.Id);
        return r;
    }

    public async Task<IEnumerable<MyUser>> GetAll()
    {
        var r = await dbContext
            .MyUsers
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
        return r;
    }

    public async Task<MyUser?> GetOneByEmail(string email)
    {
        var result = await dbContext.MyUsers.Where((item) => item.Email == email).FirstOrDefaultAsync();
        return result;
    }

    public async Task<MyUser?> GetOneByEmailOrUsername(string emailOrUsername)
    {
        return await dbContext
            .MyUsers
            .Where(
                u => u.Email == emailOrUsername
                || u.Username == emailOrUsername)
            .FirstOrDefaultAsync();
    }

    public async Task<MyUser?> GetOneById(Guid id)
    {
        return await dbContext
            .MyUsers
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<MyUser?> GetOneByUsername(string username)
    {
        var result = await dbContext.MyUsers.Where((item) => item.Username == username).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<MyRole>> GetRolesOfUser(MyUser user)
    {
        Console.WriteLine("roles of a user: " + user.Id);
        return await dbContext
            .MyRoles
            // .Include(r => r.UserRoles)
            //    .ThenInclude(ur => ur.User)
            .Where(r => r.Users.Any(ur => ur.UserId == user.Id))
            .ToListAsync();
    }

    public async Task<MyUser> RegisterNewUser(RegisterDto form)
    {
        var newUser = new MyUser()
        {
            Username = form.Username,
            Email = form.Email,
            Password = BCrypt.Net.BCrypt.EnhancedHashPassword(form.Password, 13),
            CreatedOn = DateTime.Now,
            Status = "waiting-activation"
        };

        await dbContext.AddAsync(newUser);
        await dbContext.SaveChangesAsync();

        return newUser;
    }


}