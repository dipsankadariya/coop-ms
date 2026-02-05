using bms.Data.DTOs;
using bms.Models;

namespace bms.Mappers
{
    public static class MemberAccountMapper
    {
    //convert  to dto

    public static  MemberAccountDto MaptoDto(Account memberAccount)=> new MemberAccountDto
    {
      AccountId=  memberAccount.AccountId,
      MemberId= memberAccount.MemberId,
      AccountType= memberAccount.AccountType,
      Balance= memberAccount.Balance,
      Status= memberAccount.Status,
      CreatedAt= memberAccount.CreatedAt,
    };

    //convert to entity(model)

        public static Account MaptoEntity(MemberAccountDto memberAccountDto) => new Account
        {
            AccountId = memberAccountDto.AccountId,
            MemberId = memberAccountDto.MemberId,
            AccountType = memberAccountDto.AccountType,
            Balance = memberAccountDto.Balance,
            Status = memberAccountDto.Status,
            CreatedAt = memberAccountDto.CreatedAt
        };
    }
}