using bms.Data;
using bms.Repositories.Interfaces;
using bms.ViewModels;
using Microsoft.EntityFrameworkCore;

public class HomeRepository : IHomeRepository
{
    private readonly BmsDbContext _context;

    public HomeRepository(BmsDbContext context)
    {
        _context = context;
    }

    public async Task<HomeVm> GetDashboardStatisticsAsync()
    {
        var totalMembers = await _context.Members.CountAsync();
        var activeMembers = await _context.Members.CountAsync(m => m.Status.ToLower() == "active");
        var inactiveMembers = Math.Max(0, totalMembers - activeMembers);

        
        var totalStaff = await _context.Users.CountAsync(u => u.Role.ToLower() == "staff");
        var activeStaff = await _context.Users.CountAsync(u => u.Role.ToLower() == "staff" && u.IsActive);
        var inactiveStaff = Math.Max(0, totalStaff - activeStaff);

   
        var admins = await _context.Users.CountAsync(u => u.Role.ToLower() == "admin");
        var activeAdmins = await _context.Users.CountAsync(u => u.Role.ToLower() == "admin" && u.IsActive);
        var inactiveAdmins = Math.Max(0, admins - activeAdmins);

    
        var totalAccounts = await _context.Accounts.CountAsync();
        var activeAccounts = await _context.Accounts.CountAsync(a => a.Status.ToLower() == "active");
        var inactiveAccounts = Math.Max(0, totalAccounts - activeAccounts);

       
        return new HomeVm
        {
         
            TotalMembers = totalMembers,
            ActiveMembers = activeMembers,
            InactiveMembers = inactiveMembers,

            TotalStaff = totalStaff,
            ActiveStaff = activeStaff,
            InactiveStaff = inactiveStaff,

        
            Admins = admins,
            ActiveAdmins = activeAdmins,
            InactiveAdmins = inactiveAdmins,

         
            TotalAccounts = totalAccounts,
            ActiveAccounts = activeAccounts,
            InactiveAccounts = inactiveAccounts
        };
    }
}
