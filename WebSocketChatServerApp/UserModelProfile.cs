using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketChatServer;
using WebSocketChatServerApp;

namespace WebSocketChatCoreLib
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            //From SocketUser to User
            CreateMap<SocketUser, User>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                .ForMember(dst => dst.Password, opt => opt.Ignore())
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            //From User to SocketUser
            CreateMap<User, SocketUser>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
                //.ForMember(dst => dst., opt => opt.MapFrom(src => src.Password.GetMD5Hash()))
                .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
