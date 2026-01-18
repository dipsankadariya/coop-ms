using bms.Data;
using bms.Models;
using bms.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bms.Repository.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        private readonly BmsDbContext _context;

        public MemberRepository(BmsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int memberId)
        {
            return await _context.Members.FindAsync(memberId);
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
