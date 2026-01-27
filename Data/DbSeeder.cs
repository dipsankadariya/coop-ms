using bms.Models;
using Microsoft.EntityFrameworkCore;

namespace bms.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDefaultAdminAsync(BmsDbContext context)
    {
        //check if admin user already exists
        if(await context.Users.AnyAsync(u => u.Username == "admin"))
        {
            return; // Admin already exists
        }
        
        //create default admin user
        var admin= new User
        {
            Username="admin",
            Email="admin@bms.com",
            PasswordHash=BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role="Admin",
            IsActive=true,
            CreatedDate=DateTime.UtcNow  ,
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
        }
    }
}