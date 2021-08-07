using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Models;

namespace BusinessLogic.Classes
{
    class PostsProfile : Profile
    {
        public PostsProfile()
        {
            //From UserDTO to User
            CreateMap<PostDTO, Post>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dst => dst.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.PostDate, opt => opt.MapFrom(src => src.PostDate))
                .ForMember(dst => dst.Topic, opt => opt.MapFrom(src => src.Topic))
                .ForMember(dst => dst.AuthorId, opt => opt.MapFrom(src => src.AuthorId));

            //From User to UserDTO
            CreateMap<Post, PostDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dst => dst.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.PostDate, opt => opt.MapFrom(src => src.PostDate))
                .ForMember(dst => dst.Topic, opt => opt.MapFrom(src => src.Topic))
                .ForMember(dst => dst.AuthorId, opt => opt.MapFrom(src => src.AuthorId));
        }
    }
}
