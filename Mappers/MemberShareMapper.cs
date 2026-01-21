using bms.Data.DTOs;
using bms.Models;

namespace bms.Mappers
{
    public static class MemberShareMapper
    {
        //take the membershare entity and  convert to dto
       public static MemberShareDto MapToDto(MemberShare memberShare)=>new MemberShareDto
       {
           ShareId=memberShare.ShareId,
           MemberId=memberShare.MemberId,
            Amount=memberShare.Amount,
            ContributionDate=memberShare.ContributionDate,
             ShareType=memberShare.ShareType
       };

       public static MemberShare MapToEntity(MemberShareDto memberShareDto)=>new MemberShare
       {
           ShareId =memberShareDto.ShareId,
           MemberId=memberShareDto.MemberId,
           Amount=memberShareDto.Amount,
           ContributionDate=memberShareDto.ContributionDate,
           ShareType=memberShareDto.ShareType 
       };
    }
}
