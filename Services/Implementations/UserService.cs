using bms.Data.DTOs;
using bms.Mappers;
using bms.Repositories.Interfaces;
using bms.Services.Interfaces;

namespace bms.Services.Implementations
{
    public class UserService:IUserService
{
     private readonly IUserRepository _userRepository;

     public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task ChangeUserRoleAsync(int userId, int currentUserId)
    {
        if (userId==currentUserId)
        {
            throw new Exception("You cannot change your own role.");
        }

        var user= await _userRepository.GetUserByIdAsync(userId);
        if(user==null)
        {
            throw new Exception("User not found");
        }
        //toggle btn admin and user
        user.Role= user.Role=="Admin" ? "Staff" : "Admin";

        
        //update in db
        await _userRepository.UpdateUserAsync(user);
    }
    public async Task DeleteUserAsync(int userId, int currentUserId)
    {
       if (userId == currentUserId)
        {
            throw new Exception("You cannot delete your own account.");
        }

     var user= await _userRepository.GetUserByIdAsync(userId);
        if(user==null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
      var users= await _userRepository.GetAllUsersAsync();
      //map to dto
      var usersDto=  users.Select(UserMapper.MapToDto);
      return usersDto;
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user= await _userRepository.GetUserByIdAsync(userId);
        if(user==null) return null;

        //convert to dto
        var userDto= UserMapper.MapToDto(user);
        return userDto;
    }

    public async Task ToggleUserStatusAsync(int userId, int currentUserId)
    {
        if (userId == currentUserId)
        {
            throw new Exception("You cannot change your own status.");
        }

        var user= await _userRepository.GetUserByIdAsync(userId);
        if(user==null)
        {
            throw new Exception("User not found");
        }
        //chage the status of the user to inactive
        user.IsActive=!user.IsActive;
        //update in db
        await _userRepository.UpdateUserAsync(user);
    }
    }
}
