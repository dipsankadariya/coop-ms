using bms.Data.DTOs;
using bms.Models;
using bms.Repositories.Interfaces;
using bms.Repository.Interfaces;
using bms.Services.Interfaces;
using bms.Mappers;

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
        public async Task AddMemberShareAsync(MemberShareDto memberShareDto)
        {
            var member = await _memberRepository.GetByIdAsync(memberShareDto.MemberId);  
            if(member==null)
            {
                throw new Exception("Member not found");
            }
            if(member.Status != "Active")
            {
                throw new Exception("Member must be active inorder to contribute shares");
            }
            if (memberShareDto.Amount <= 0)
            {
                throw new Exception("Share Amount must be greater than 0");
            }

            var entity= MemberShareMapper.MapToEntity(memberShareDto);    
            entity.ContributionDate=DateTime.Now;
            await _memberShareRepository.AddAsync(entity);
            
        }

        public async Task<IEnumerable<MemberShareDto>> GetAllMemberSharesByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null) {
                throw new Exception("Member not found");
            }

            var memberShares= await _memberShareRepository.GetAllForMemberByIdAsync(memberId);
            var memberShareDto=memberShares.Select(MemberShareMapper.MapToDto);
            return memberShareDto;

        }

        public async Task<decimal> GetTotalShareByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
            {
                return 0; // Return 0 instead of throwing exception
            }

            var total = await _memberShareRepository.GetTotalForMemberAsync(memberId);
            return total;
        }
    }
}
