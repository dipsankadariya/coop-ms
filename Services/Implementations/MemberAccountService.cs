using bms.Data.DTOs;
using bms.Mappers;
using bms.Models;
using bms.Repositories.Interfaces;
using bms.Repository.Interfaces;
using bms.Services.Interfaces;

namespace bms.Services.Implementations
{
    public class MemberAccountService : IMemberAccountService
    {
        private  readonly IMemberAccountRepository _memberAccountRepository;

        private readonly IMemberRepository _memberRepository;

        private readonly IMemberShareRepository _memberShareRepository;

        public  MemberAccountService(IMemberAccountRepository memberAccountRepository, IMemberRepository memberRepository, IMemberShareRepository memberShareRepository)
        {
            _memberAccountRepository = memberAccountRepository;
            _memberRepository = memberRepository;
            _memberShareRepository = memberShareRepository;
        }

        public async Task AddMemberAccountAsync(MemberAccountDto account)
        {
            var member = await _memberRepository.GetByIdAsync(account.MemberId);

            if(member == null)
            {
                throw new Exception("Member not found");
            }

            if(member.Status != "Active")
            {
                throw new Exception("Cannot create account for inactive member");
            }
            
            //##rule://check if member has at least 1 share bought
            
            //get all membershares
            var memberShares = await _memberShareRepository.GetAllForMemberByIdAsync(account.MemberId);
            if(memberShares == null || !memberShares.Any())
            {
                throw new Exception("Member must have at least one share bought to create an account");
            }
            // Set initial values for new account
            account.AccountId = 0; 
            account.Status = string.IsNullOrEmpty(account.Status) ? "Active" : account.Status;
            account.CreatedAt = DateTime.Now;

            // Convert the incoming dto into entity before saving into db
            var entity = MemberAccountMapper.MaptoEntity(account);
            await _memberAccountRepository.AddAsync(entity);
        }

    public async Task<MemberAccountDto?> GetAccountByIdAsync(int accountId)
    {
       var memberAccount= await _memberAccountRepository.GetByIdAsync(accountId);
        if(memberAccount==null) return null;

        //map the entity to dto before returning
        return MemberAccountMapper.MaptoDto( memberAccount);
    }

        public async Task<IEnumerable<MemberAccountDto>> GetAllAccountsByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if(member == null || member.Status != "Active")
            {
                throw new Exception("Either member is not  active or member not found.");
            }
            var memberAccounts = await _memberAccountRepository.GetAllByMemberIdAsync(memberId);
            return memberAccounts.Select(account => MemberAccountMapper.MaptoDto(account)).ToList();
        }

        public async Task UpdateMemberAccountAsync(MemberAccountDto account)
        {
            // Check if account exists
            var existingAccount = await _memberAccountRepository.GetByIdAsync(account.AccountId);
            if(existingAccount == null)
            {
                throw new Exception("Account not found");
            }

            // Verify account belongs to the member
            if(existingAccount.MemberId != account.MemberId)
            {
                throw new Exception("Account does not belong to this member");
            }

            // Check if member exists
            var member = await _memberRepository.GetByIdAsync(account.MemberId);
            if(member == null)
            {
                throw new Exception("Member not found");
            }

            // Business rule: Cannot deactivate account with non-zero balance
            if(account.Status == "Inactive" && existingAccount.Balance != 0)
            {
                throw new Exception($"Cannot deactivate account with non-zero balance of {existingAccount.Balance}");
            }

            // Apply status change (only status can be updated, not balance/type/memberId)
            existingAccount.Status = account.Status;

            await _memberAccountRepository.UpdateAsync(existingAccount);
        }
    }
}
