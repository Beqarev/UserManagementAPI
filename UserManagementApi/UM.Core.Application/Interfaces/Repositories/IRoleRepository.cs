using UM.Core.Application.DTOs;
using UM.Core.Domain.Models;

namespace UM.Core.Application.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<bool> CheckAllAsync(IEnumerable<int> roleIds);
}