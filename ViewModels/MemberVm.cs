namespace bms.ViewModels.Member
{
    public class MemberVm
    {
     public int MemberId { get; set; }

    public string MemberName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public long PhoneNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";

        public int ShareCount { get; set; } = 0;

        public IEnumerable<MemberVm> Members { get; set; } = new List<MemberVm>();
}
}