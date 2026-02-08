using bms.Data.DTOs;
using bms.ViewModels.MemberShare;

namespace bms.Mappers.ViewModelMappers
{
    public static class MemberShareVmMapper
    {
        //convert dto to viewmodel
        public static MemberShareVm MapDtoToVm(MemberShareDto memberShareDto) => new MemberShareVm
        {
            ShareId = memberShareDto.ShareId,
            MemberId = memberShareDto.MemberId,
            Amount = memberShareDto.Amount,
            ShareType = memberShareDto.ShareType,
            ContributionDate = memberShareDto.ContributionDate
        };

        //convert viewmodel to dto
        public static MemberShareDto MapVmToDto(MemberShareVm memberShareVm) => new MemberShareDto
        {
            MemberId = memberShareVm.MemberId,
            Amount = memberShareVm.Amount,
            ShareType = memberShareVm.ShareType!,
        };
    }
}