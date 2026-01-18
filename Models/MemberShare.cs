using System;
using System.Collections.Generic;

namespace bms.Models;

public partial class MemberShare
{
    public long ShareId { get; set; }

    public int MemberId { get; set; }

    public decimal Amount { get; set; }

    public DateTime ContributionDate { get; set; }

    public string? ShareType { get; set; }

    public virtual Member Member { get; set; } = null!;
}
