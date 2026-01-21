using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bms.Data.DTOs
{
    public class MemberShareDto
    {
    public long ShareId { get; set; }

    public int MemberId { get; set; }

    public decimal Amount { get; set; }

    public DateTime ContributionDate { get; set; }

    public string? ShareType { get; set; }
    
    }
}