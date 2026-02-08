using bms.ViewModels.Member;

namespace bms.ViewModels.MemberShare
{
    public class MemberShareVm
    {
        public int MemberId { get; set; }

        public long ShareId { get; set; }

        public decimal Amount { get; set; }

        public string ShareType { get; set; } = string.Empty;

        public DateTime ContributionDate { get; set; }

        public IEnumerable<MemberVm> MemberList { get; set; } = new List<MemberVm>();

        public IEnumerable<MemberShareVm> MemberShares { get; set; } = new List<MemberShareVm>();
    }
}