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
    }
}
