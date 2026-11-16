using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> GetById(int id);
    Task<User?> GetByUserName(string username);
    Task<List<User>> GetAll();
    Task Update(User user);
}