using Microsoft.AspNetCore.Mvc.Rendering;

namespace bms.ViewModels.MemberAccount
{
    public class MemberAccountVm
    {
        public int AccountId { get; set; }

        public int MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public IEnumerable<AccountTransactionVm> Transactions { get; set; } = new List<AccountTransactionVm>();

        // For dropdown in Create view
        public IEnumerable<SelectListItem> MemberList { get; set; } = new List<SelectListItem>();
    }
}