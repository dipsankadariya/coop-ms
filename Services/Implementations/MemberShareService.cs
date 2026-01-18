using bms.Models;
using bms.Repositories.Interfaces;
using bms.Repository.Interfaces;
using bms.Services.Interfaces;

namespace bms.Services.Implementations
{
    public class MemberShareService : IMemberShareService
    {
        private readonly IMemberShareRepository _memberShareRepository;

        private readonly IMemberRepository _memberRepository;

        public MemberShareService(IMemberShareRepository memberShareRepository, IMemberRepository memberRepository)
        {
           _memberShareRepository = memberShareRepository;
            _memberRepository = memberRepository;
        }
        public async Task AddMemberShareAsync(int memberId, decimal shareAmount, string shareType)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);  
            if(member==null)
            {
                throw new Exception("Member not found");
            }
            if(member.Status != "Active")
            {
                throw new Exception("Member must be active inorder to contribute shares");
            }
            if (shareAmount < 0)
            {
                throw new Exception("Share Amount must be greater than 0");
            }

            var memberShare = new MemberShare
            {
                MemberId = memberId,
                Amount = shareAmount,
                ShareType = shareType
            };
            
            await _memberShareRepository.AddAsync(memberShare);

        }

        public async Task<IEnumerable<MemberShare>> GetAllMemberSharesByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null) {
                throw new Exception("Member not found");
            }

            return await _memberShareRepository.GetAllForMemberByIdAsync(memberId);

        }

        public async Task<decimal> GetTotalShareByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
            {
                throw new Exception("Member not found");
            }

            return await _memberShareRepository.GetTotalForMemberAsync(memberId);
        }
    }
}
