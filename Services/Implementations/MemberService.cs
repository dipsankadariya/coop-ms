using bms.Data.DTOs;
using bms.Mappers;
using bms.Models;
using bms.Repository.Interfaces;
using bms.Services.Interfaces;

namespace bms.Services.Implementations
{
    public class MemberService:IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task AddNewMemberAsync(MemberDto member)
        {
            //map the incoming member dto to entity
           var entity= MemberMapper.MapToEntity(member);
            await _memberRepository.AddAsync(entity);
        }


        public async Task DeleteMemberAsync(int id)
        {
           await  _memberRepository.DeleteAsync(id);

        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members= await _memberRepository.GetAllAsync();
            return members.Select(member=> MemberMapper.MapToDto(member));

        }
        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var member= await _memberRepository.GetByIdAsync(id);
            if(member==null) return null;

           return MemberMapper.MapToDto(member);
        }

        public async Task UpdateMemberAsync(MemberDto member)
        {
            var existingMember= await _memberRepository.GetByIdAsync(member.MemberId);
            if(existingMember==null)
            {
                throw new Exception("Member not found");
            }

            //update the member details
            existingMember.FullName= member.FullName;
            existingMember.Email= member.Email;
            existingMember.PhoneNumber= member.PhoneNumber;
            existingMember.Status= member.Status ?? "Active";
            existingMember.Address= member.Address;

            await _memberRepository.UpdateAsync(existingMember);
        }
    }
}