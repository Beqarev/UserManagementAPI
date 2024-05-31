using UM.Core.Domain.Models;

namespace UM.Core.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IQueryable<User>> Filter(string userName, int roleId);
    Task<User> GetUserByUserName(string userName);  
    Task<User> GetUserById(int userId);
    Task CreateUser(User user);
    Task UpdateUser(int id, User user);
}