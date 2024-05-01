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
        return await dbContext.MyUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<MyUser?> GetOneByUsername(string username)
    {
        var result = await dbContext.MyUsers.Where((item) => item.Username == username).FirstOrDefaultAsync();
        return result;
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