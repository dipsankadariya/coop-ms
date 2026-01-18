using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int MemberId { get; set; }

    public int AccountId { get; set; }

    public string? TransactionType { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Notes { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
