using bms.Models;

namespace bms.Services.Interfaces
{
    public interface IMemberShareService
    {
        Task AddMemberShareAsync(int memberId, decimal shareAmount, string shareType);

        Task<IEnumerable<MemberShare>> GetAllMemberSharesByMemberIdAsync(int memberId);
        Task <decimal> GetTotalShareByMemberIdAsync(int memberId);

    }
}
