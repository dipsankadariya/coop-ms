namespace bms.ViewModels.MemberAccount
{
    public class MemberAccountVm
    {
        public int AccountId { get; set; }

        public int MemberId { get; set; }

        public string AccountType { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}