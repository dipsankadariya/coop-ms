using bms.ViewModels;

namespace bms.Repositories.Interfaces
{
public interface IHomeRepository
{
    Task <HomeVm> GetDashboardStatisticsAsync();
}
}
