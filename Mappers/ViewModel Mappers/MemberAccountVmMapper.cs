using bms.Data.DTOs;
using bms.ViewModels.MemberAccount;

namespace bms.Mappers.ViewModelMappers
{
    public static class MemberAccountVmMapper
    {
        // convert dto to viewmodel

        public static MemberAccountVm MapDtoToVm(MemberAccountDto memberAccountDto) => new MemberAccountVm
        {
         AccountId=memberAccountDto.AccountId,
         MemberId=memberAccountDto.MemberId,
        AccountType=memberAccountDto.AccountType,
        Balance=memberAccountDto.Balance,
        Status=memberAccountDto.Status,
        CreatedAt=memberAccountDto.CreatedAt,
        };

        //convert viewmodel to dto

        public static MemberAccountDto MapVmToDto(MemberAccountVm memberAccountVm) => new MemberAccountDto
        {
         AccountId=memberAccountVm.AccountId,
         MemberId=memberAccountVm.MemberId,
        AccountType=memberAccountVm.AccountType,
        Balance=memberAccountVm.Balance,
        Status=memberAccountVm.Status,
        CreatedAt=memberAccountVm.CreatedAt,
        };

    }
}