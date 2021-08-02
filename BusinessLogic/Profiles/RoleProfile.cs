using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Models;

namespace BusinessLogic.Classes
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            //From RoleDTO to Role
            CreateMap<RoleDTO, Role>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RoleName, opt => opt.MapFrom(src => src.RoleName));

            //From Role to RoleDTO
            CreateMap<Role, RoleDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.RoleName, opt => opt.MapFrom(src => src.RoleName));
        }
    }
}
