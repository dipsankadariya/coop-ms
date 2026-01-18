using bms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bms.Repositories.Interfaces
{
    public interface IMemberShareRepository
    {
        // get all share contributions for a specific member
        Task<IEnumerable<MemberShare>> GetAllForMemberByIdAsync(int memberId);

        // add a new member share contribution
        Task AddAsync(MemberShare memberShare);

        // get the total amount contributed by a member
        Task<decimal> GetTotalForMemberAsync(int memberId);
    }
}
