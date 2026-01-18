using bms.Data;
using bms.Models;
using bms.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bms.Repositories.Implementations
{
    public class MemberShareRepository : IMemberShareRepository
    {
        private readonly BmsDbContext _context;

        public MemberShareRepository(BmsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MemberShare memberShare)
        {
            _context.MemberShares.Add(memberShare);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberShare>> GetAllForMemberByIdAsync(int memberId)
        {
            return await _context.MemberShares.Where(s => s.MemberId == memberId).OrderByDescending(s => s.ContributionDate).ToListAsync();
        }

        public async Task<decimal> GetTotalForMemberAsync(int memberId)
        {
            return await _context.MemberShares.Where(s => s.MemberId == memberId).SumAsync(s => s.Amount);
        }
    }
}
