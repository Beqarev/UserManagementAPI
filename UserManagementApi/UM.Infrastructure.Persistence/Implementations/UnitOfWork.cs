using UM.Core.Application.Interfaces;
using UM.Core.Application.Interfaces.Repositories;
using UM.Infrastructure.Persistence.Data;
using UM.Infrastructure.Persistence.Implementations.Repositories;

namespace UM.Infrastructure.Persistence.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private IUserRepository userRepository;
    private IRoleRepository roleRepository;

    private readonly DataContext context;
    
    public UnitOfWork(DataContext context)
    {
        this.context = context;
    }
    public IRoleRepository RoleRepository => roleRepository ??= new RoleRepository(context);
    public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
    public int Save() => context.SaveChanges();

    public async Task<int> SaveAsync() => await context.SaveChangesAsync();
}