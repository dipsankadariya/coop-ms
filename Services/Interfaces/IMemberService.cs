using bms.Models;

namespace bms.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();

        Task<Member> GetMemberByIdAsync(int id);

        Task AddNewMemberAsync(Member member);

        Task DeleteMemberAsync(int id);
        Task UpdateMemberAsync(Member member);
    }
}
