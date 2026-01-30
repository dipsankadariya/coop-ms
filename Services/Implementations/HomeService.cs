using bms.Repositories.Interfaces;
using bms.Services.Interfaces;
using bms.ViewModels;

namespace bms.Services.Implementations
{
public class HomeService : IHomeService
{
    public readonly IHomeRepository _homeRepository;
    public HomeService(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }
    public async Task<HomeVm> GetDashboardStatisticsAsync()
    {
        return await _homeRepository.GetDashboardStatisticsAsync();
    }
}
}