using bms.Models;

namespace bms.Repository.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(int memberId);
        Task<IEnumerable<Member>> GetAllAsync();
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int memberId);
    }

}