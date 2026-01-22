using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<MemberShare> MemberShares { get; set; } = new List<MemberShare>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
