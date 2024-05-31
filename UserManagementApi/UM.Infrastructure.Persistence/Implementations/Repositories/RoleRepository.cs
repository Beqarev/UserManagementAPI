using Microsoft.EntityFrameworkCore;
using UM.Core.Application.DTOs;
using UM.Core.Application.Interfaces.Repositories;
using UM.Core.Domain.Models;
using UM.Infrastructure.Persistence.Data;

namespace UM.Infrastructure.Persistence.Implementations.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> CheckAllAsync(IEnumerable<int> roleIds)
    {
        var existsIds = await _context.Roles.Select(x => x.Id).ToListAsync();
        return await Task.Run(() => roleIds.All(x => existsIds.Any(y => y == x)));
    }
}