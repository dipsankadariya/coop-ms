using BCrypt.Net;
using bms.Data.DTOs;
using bms.Mappers;
using bms.Models;
using bms.Repositories.Interfaces;
using bms.Services.Interfaces;

namespace bms.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _userRepository.EmailExistsAsync(email);
    }

    public  async Task<UserDto> RegisterUserAsync(string username, string email, string password)
    {
        var usernameExists = await _userRepository.UsernameExistsAsync(username);
        if (usernameExists)
        {
            throw new Exception("Username already exists");
        }
        var emailExists = await _userRepository.EmailExistsAsync(email);
        if (emailExists)
        {
            throw new Exception("Email already exists");
        }

        var entity= new User
        {
            Username=username,
            Email=email,
            PasswordHash=BCrypt.Net.BCrypt.HashPassword(password),
            Role="User",
            IsActive=true,
            CreatedDate=DateTime.Now,
        };
        await _userRepository.AddUserAsync(entity);

        var userDto= UserMapper.MapToDto(entity);
        return userDto;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _userRepository.UsernameExistsAsync(username);
    }

    public async Task<UserDto> ValidateUserAsync(string username, string password)
    {
      var user= await _userRepository.GetUserByUsernameAsync(username);

      if(user==null || !user.IsActive) {
        throw new Exception("User not found or inactive");
      }
      

     var passwordValid=BCrypt.Net.BCrypt.Verify(password,user.PasswordHash);
        if(!passwordValid)
        {
            throw new Exception("Invalid password");
        }
       //after checking the user and password we map to dto and return userdto so  that controller can use it
        var userdto= UserMapper.MapToDto(user);
        return userdto;  
    }
    }
}
