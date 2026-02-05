using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }
    
    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal BalanceAfter { get; set; }
    public string? Notes { get; set; }

    public virtual Account Account { get; set; } = null!;
}
