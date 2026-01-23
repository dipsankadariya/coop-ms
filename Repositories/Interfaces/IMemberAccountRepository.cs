using bms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bms.Repositories.Interfaces
{
    public interface IMemberAccountRepository
    {
    // get all accounts of a member
    Task<IEnumerable<Account>> GetAllByMemberIdAsync(int memberId);

    // get single account by account id
    Task<Account?> GetByIdAsync(int accountId);

    // open new account
    Task AddAsync(Account account);

        // update account (status / type only)
        Task UpdateAsync(Account account);
    }
}
