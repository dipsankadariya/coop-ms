using Microsoft.AspNetCore.Mvc.Rendering;

namespace bms.ViewModels
{
    public class AccountTransactionVm
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int MemberId { get; set; }
        public string TransactionType { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Notes { get; set; }
        public string MemberName { get; set; } = string.Empty;

        // For dropdowns
        public IEnumerable<SelectListItem> MemberList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> AccountList { get; set; } = new List<SelectListItem>();
    }
}
