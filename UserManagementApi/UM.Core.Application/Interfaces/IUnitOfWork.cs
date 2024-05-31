using UM.Core.Application.Interfaces.Repositories;

namespace UM.Core.Application.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    
    public int Save();
    public Task<int> SaveAsync();
}