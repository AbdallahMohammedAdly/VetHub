

using AutoMapper;
using VetHub02.Core.Entities;
using VetHub02.Core.Entities.identity;
using VetHub02.DTO;

namespace VetHub02.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Join , JoinDto>().ReverseMap();
            
            CreateMap<ContactUs,ContactUsDto>().ReverseMap();
            CreateMap<History ,HistoryDto>()
                   .ForMember(A => A.ImageUrl, O => O.MapFrom<ImageUrlHistory>())
                   .ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Article, ArticleDto>()
                .ForMember(A => A.ImageUrl , O => O.MapFrom<ImageUrlArticle>())
                .ReverseMap();
           
            CreateMap<RegisterDto, AppUser>()
                .ForMember(des => des.DisplayName , O => O.MapFrom(src => src.Name) );

            CreateMap<User, UserDto>()
                .ForMember(U=>U.Name , O => O.MapFrom(src => src.Name))
                                .ReverseMap();
            CreateMap<Comment, CommentDto>()
                .ForMember(C => C.Title , O => O.MapFrom(src => src.Title))
                .ForMember(C => C.Content , O => O.MapFrom(src => src.Content)).ReverseMap();

            CreateMap<User, RegisterDto>()
              
                .ReverseMap();
           
        }
    }
}
