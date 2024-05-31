using Microsoft.EntityFrameworkCore;
using UM.Core.Application.Interfaces.Repositories;
using UM.Core.Domain.Models;
using UM.Infrastructure.Persistence.Data;

namespace UM.Infrastructure.Persistence.Implementations.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    
    public UserRepository(DataContext context) : base(context) { }


    public Task<IQueryable<User>> Filter(string userName, int roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Username == userName);
        return await Task.FromResult(user);
    }

    public async Task<User> GetUserById(int userId)
    {
        var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == userId);
        return await Task.FromResult(user);
    }

    public async Task CreateUser(User user)
    {
        await Task.FromResult(_context.Users.Add(user));
    }

    public async Task UpdateUser(int id, User user)
    {
        user.Id = id;
        var existing = _context.Users.First(x => x.Id == id);
        await Task.FromResult(existing);
        user.Password = string.IsNullOrWhiteSpace(user.Password) ? existing.Password : user.Password;
        _context.Entry(existing).CurrentValues.SetValues(user);
    }
}