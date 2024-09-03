using AutoMapper;
using VetHub02.Core.Entities;
using VetHub02.DTO;

namespace VetHub02.Helpers
{
    public class ImageUrlHistory : IValueResolver<History, HistoryDto, string>
    {
        private readonly IConfiguration configuration;

        public ImageUrlHistory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(History source, HistoryDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
                return $"{configuration["ApiUrl"]}{source.ImageUrl}";



            return string.Empty;
        }
    }
}
