using bms.Data.DTOs;
using bms.Models;

namespace bms.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();

        Task<MemberDto?> GetMemberByIdAsync(int id);

        Task AddNewMemberAsync(MemberDto member);

        Task DeleteMemberAsync(int id);
        Task UpdateMemberAsync(MemberDto member);
    }
}
