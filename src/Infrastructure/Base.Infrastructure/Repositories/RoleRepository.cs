using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities;
using Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNameAsync(string roleName)
        => await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

    public async Task<IEnumerable<Role>> GetAllAsync()
        => await _context.Roles.ToListAsync();
}