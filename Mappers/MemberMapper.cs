using bms.Data.DTOs;
using bms.Models;

namespace bms.Mappers
{
    public static class MemberMapper
{   //Without using static keyword, every time we  wanted to map, we would  have to create a new MemberMapper object:something like thsi:
    //var mapper = new MemberMapper();
    //var dto = mapper.MapToDto(memberEntity);
    //With static, you can call the method directly without creating an object:
    //var dto = MemberMapper.MapToDto(memberEntity);

    // maptoDto-> this will take the member entity(ie the Member model ie ef Member table ) and convert to memberdto. used  when reading form database. returns a MemberDto
    public static MemberDto MapToDto(Member member)=> new MemberDto
    {
        MemberId=member.MemberId,
        FullName=member.FullName,
        Address=member.Address,
        PhoneNumber=member.PhoneNumber,
        Email=member.Email,
        Status=member.Status ??"Active"
    };
    
    // maptoEntity-> this will take the memberDto and convert to member entity. used  when when adding or updating the data to the database. return member entity
    public static Member MapToEntity(MemberDto memberDto)=> new Member
    {
        MemberId=memberDto.MemberId,
        FullName=memberDto.FullName,
        Address=memberDto.Address,
        PhoneNumber=memberDto.PhoneNumber,
        Email=memberDto.Email,
        Status=memberDto.Status??"Active"
    };
    }
}