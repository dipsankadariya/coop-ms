using bms.Data.DTOs;
using bms.Models;

namespace bms.Services.Interfaces
{
    public interface IMemberShareService
    {
        Task AddMemberShareAsync(MemberShareDto memberShareDto);

        Task<IEnumerable<MemberShareDto>> GetAllMemberSharesByMemberIdAsync(int memberId);
        Task <decimal> GetTotalShareByMemberIdAsync(int memberId);

    }
}
