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

        public async Task AddNewMemberAsync(Member member)
        {
            member.Status = "Active";
            await _memberRepository.AddAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
             return   await _memberRepository.GetAllAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task UpdateMemberAsync(Member member)
        {
           
            await _memberRepository.UpdateAsync(member);
        }
    }
}
