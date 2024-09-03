using AutoMapper;
using VetHub02.Core.Entities;
using VetHub02.DTO;

namespace VetHub02.Helpers
{
    public class ImageUrlArticle : IValueResolver<Article, ArticleDto, string> 
    {
        private readonly IConfiguration configuration;

        public ImageUrlArticle(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Article source, ArticleDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
                return $"{configuration["ApiUrl"]}Images{source.ImageUrl}"; 
            


            return string.Empty ;
        }
    }
}
