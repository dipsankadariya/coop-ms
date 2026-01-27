using bms.Data;
using bms.Models;
using bms.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bms.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly bms.Data.BmsDbContext _context;
        
        public UserRepository(bms.Data.BmsDbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user= await _context.Users.FindAsync(userId);
            if (user != null)
            {
               _context.Users.Remove(user);
            await _context.SaveChangesAsync();   
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(user=>user.Email==email);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users= await _context.Users.OrderByDescending(user=>user.CreatedDate).ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user=>user.Email==email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user= await _context.Users.FirstOrDefaultAsync(user=>user.Username==username);
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            return _context.Users.AnyAsync(user=>user.Username==username);
        }
    }
}