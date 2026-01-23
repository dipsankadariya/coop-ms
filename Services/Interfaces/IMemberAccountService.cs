using bms.Data.DTOs;
using bms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bms.Services.Interfaces
{
    public interface IMemberAccountService
    {
    // get all accounts of a member
    Task<IEnumerable<MemberAccountDto>> GetAllAccountsByMemberIdAsync(int memberId);

    // get single account
    Task<MemberAccountDto?> GetAccountByIdAsync(int accountId);

    // open new account (with validations)
    Task AddMemberAccountAsync(MemberAccountDto account);

        // update account (close / freeze / change type)
        Task UpdateMemberAccountAsync(MemberAccountDto account);
    }
}
