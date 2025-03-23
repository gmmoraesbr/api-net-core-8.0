using Base.Domain.Contracts.Repositories;
using Base.Domain.Entities;
using Base.Domain.ValueObjects;
using Base.Infrastructure.Data;
using Base.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Base.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
        => await _context.Users.Include(u => u.Roles).ThenInclude(r => r.RoleId)
                               .FirstOrDefaultAsync(u => u.UserName == username);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id)
        => await _context.Users.FindAsync(id);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ValidatePasswordAsync(User user, string password)
    {
        return Task.FromResult(PasswordHasher.Verify(user.PasswordHash, password));
        //var hash = PasswordHasher.Hash(password);
        //return Task.FromResult(user.PasswordHash == hash);
    }
}