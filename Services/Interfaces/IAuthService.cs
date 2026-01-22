using bms.Data.DTOs;

namespace bms.Services.Interfaces
{
    /*
     * the reason why are passing individual parameters instead of passing UserDto is because:
     * - Forms have plain text passwords, but UserDto needs hashed passwords
     * - Service receives plain values, hashes the password, then creates User entity
     * - Keeps password hashing logic in the service layer (not in controller)
     * 
     * Flow: Form → Controller → Service (hashes password) → Repository → Database
     */
    public interface IAuthService
    {
        Task<UserDto> ValidateUserAsync(string username, string password);
        Task<UserDto> RegisterUserAsync(string username, string email, string password);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}