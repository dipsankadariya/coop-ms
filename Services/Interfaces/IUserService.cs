using bms.Data.DTOs;

namespace bms.Services.Interfaces
{
    public interface IUserService
    //by user , here we mean staffs
    {
         //get all users(for admin panel list)
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        
        //aah, get single user details ->view or edit
        Task<UserDto?> GetUserByIdAsync(int userId);

        //toggle user active/inactive status
        Task ToggleUserStatusAsync(int userId,int currentUserId); // The user being toggled,currentUserId is the one performing the action ie admin.just for safety check.

        //to change the role , admin->usere , and vice versa.
        Task ChangeUserRoleAsync(int userId, int currentUserId);

        //delete user account
        Task DeleteUserAsync(int userId,int currentUserId);


        //all modification takes CurrentUserId so that  if in scenario  if admin mistakley clicks wrong button and tries to delete or deactivate self , we can prevent that.
        //it checks if userId==currentUserId , if thats true , then we can understand that admin is trying to perform action on self , so we prevent that.


    }
}