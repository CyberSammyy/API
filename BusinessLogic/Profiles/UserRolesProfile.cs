using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Models;

namespace BusinessLogic.Classes
{
    public class UserRolesProfile : Profile
    {
        public UserRolesProfile()
        {
            //From RoleDTO to Role
            CreateMap<UserRolesDTO, UserRoles>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId));

            //From Role to RoleDTO
            CreateMap<UserRoles, UserRolesDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
