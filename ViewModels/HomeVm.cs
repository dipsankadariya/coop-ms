namespace bms.ViewModels
{
    public class HomeVm
    {
        // ----- Members -----
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int InactiveMembers { get; set; }

        // ----- Staff -----
        public int TotalStaff { get; set; }
        public int ActiveStaff { get; set; }
        public int InactiveStaff { get; set; }

        // ----- Admins -----
        public int Admins { get; set; }
        public int ActiveAdmins { get; set; }
        public int InactiveAdmins { get; set; }

        // ----- Accounts -----
        public int TotalAccounts { get; set; }
        public int ActiveAccounts { get; set; }
        public int InactiveAccounts { get; set; }

        // ----- Transaction Stats (for chart) -----
        public int TotalTransactions { get; set; }
        public int DepositCount { get; set; }
        public int WithdrawalCount { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }

        // ----- Account Type Distribution -----
        public int SavingsAccounts { get; set; }
        public int CurrentAccounts { get; set; }
        public int FixedDepositAccounts { get; set; }
        public int LoanAccounts { get; set; }
    }
}
