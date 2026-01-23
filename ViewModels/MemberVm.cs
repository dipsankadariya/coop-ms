namespace bms.ViewModels.Member
{
    public class MemberVm
    {
     public int MemberId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public long PhoneNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Status { get; set; } = "Active";
}
}