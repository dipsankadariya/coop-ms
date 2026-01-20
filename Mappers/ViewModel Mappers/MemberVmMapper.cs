using bms.Data.DTOs;

namespace bms.ViewModels.Member
{
    public static class MemberVmMapper
    {
       // DTO → ViewModel (for displaying in views)
       public static MemberVm MapDtoToViewModel(MemberDto memberDto)
        {
         return new MemberVm
            {
                MemberId = memberDto.MemberId,
                FullName = memberDto.FullName,
                Address = memberDto.Address,
                PhoneNumber = memberDto.PhoneNumber,
                Email = memberDto.Email,
                Status = memberDto.Status ?? "Active"
            };   
        }

         // ViewModel → DTO (for form submission)
        public static MemberDto MapViewModelToDto(MemberVm memberVm)
        {
            return new MemberDto
            {
                MemberId = memberVm.MemberId,
                FullName = memberVm.FullName,
                Address = memberVm.Address,
                PhoneNumber = memberVm.PhoneNumber,
                Email = memberVm.Email,
                Status = memberVm.Status ?? "Active"
            };
        }

    }
}