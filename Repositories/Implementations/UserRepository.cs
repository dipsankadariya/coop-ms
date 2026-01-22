using bms.Data;
using bms.Models;
using bms.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bms.Repositories.Implementations
{
    public class UserRepository : IUserRepository
{
    private readonly BmsDbContext _context;
    public UserRepository(BmsDbContext context)
    {
        _context = context;
    }
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
         await _context.SaveChangesAsync();
        
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
         return await _context.Users.AnyAsync(user=>user.Email==email);
        
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
       return  await  _context.Users.FirstOrDefaultAsync(user=>user.Email==email);
    }

    public async Task<User?> GetUserbyIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(user=>user.Username==username);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(user=>user.Username==username);
    }
    }
}