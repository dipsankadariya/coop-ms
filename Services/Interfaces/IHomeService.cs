using bms.ViewModels;

namespace bms.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeVm> GetDashboardStatisticsAsync();
    }
}
