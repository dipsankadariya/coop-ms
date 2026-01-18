using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int MemberId { get; set; }

    public string AccountType { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Status { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
