using bms.Models;

namespace bms.Repositories.Interfaces
{
    public interface IUserRepository
    {
    
    Task AddUserAsync(User user);
     Task<User?>GetUserByIdAsync(int userId);
    Task<User?> GetUserByUsernameAsync(string username);

    Task<User?>GetUserByEmailAsync(string email);

    Task<bool> UsernameExistsAsync(string username);

    Task<bool>EmailExistsAsync(string email);

    Task<IEnumerable<User>> GetAllUsersAsync();
    
    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(int userId);
    }
}