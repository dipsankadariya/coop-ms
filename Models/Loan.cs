using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class Loan
{
    public int LoanId { get; set; }

    public int MemberId { get; set; }

    public string LoanType { get; set; } = null!;

    public decimal PrincipalAmount { get; set; }

    public decimal InterestRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
