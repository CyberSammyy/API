using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Models;

namespace BusinessLogic.Classes
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //From UserDTO to User
            CreateMap<UserDTO, User>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Password, opt => opt.Ignore())
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            //From User to UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.PasswordHash, opt => opt.MapFrom(src => src.Password.GetMD5Hash()))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
