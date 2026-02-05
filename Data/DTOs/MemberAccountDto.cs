using bms.Models;

namespace bms.Data.DTOs
{
    public class MemberAccountDto
    {
    public int AccountId { get; set; }

    public int MemberId { get; set; }
 
    public string MemberName { get; set; } = string.Empty;
    public string AccountType { get; set; } =string.Empty;

    public decimal Balance { get; set; }

    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }  
    }
}