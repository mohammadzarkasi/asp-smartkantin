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
            Password = form.Password,
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now,
            Status = "waiting-activation"
        };

        await dbContext.AddAsync(newUser);
        await Save();

        return newUser;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }
}