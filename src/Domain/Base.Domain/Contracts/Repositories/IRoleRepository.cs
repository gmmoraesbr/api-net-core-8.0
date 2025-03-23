using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Contracts.Repositories;

using Base.Domain.Entities;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
    Task<IEnumerable<Role>> GetAllAsync();
}

