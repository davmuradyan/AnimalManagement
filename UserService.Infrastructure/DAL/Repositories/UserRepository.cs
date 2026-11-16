using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.Database;

namespace UserService.Infrastructure.DAL.Repositories;

public class UserRepository(MainDbContext context) : IUserRepository
{
    public async Task Create(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetById(int id)
    {
        var user = await context.Users.FindAsync(id);
        return user;
    }

    public async Task<User?> GetByUserName(string username)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task Update(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}