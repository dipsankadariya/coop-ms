using bms.Data.DTOs;
using bms.Models;

namespace bms.Mappers
{
    public static class UserMapper
{
    //map  to dto
     public static UserDto MapToDto(User user)=> new UserDto
    {
        UserId=user.UserId,
        Username=user.Username,
        Email=user.Email,
        PasswordHash=user.PasswordHash,
        Role=user.Role,
        IsActive=user.IsActive,
        CreatedDate=user.CreatedDate
    };

    //map to entity
    public static User MapToEntity(UserDto userDto)=> new User
    {
        UserId=userDto.UserId,
        Username=userDto.Username,
        Email=userDto.Email,
        PasswordHash=userDto.PasswordHash,
        Role=userDto.Role,
        IsActive=userDto.IsActive,
        CreatedDate=userDto.CreatedDate

    };
    }
}