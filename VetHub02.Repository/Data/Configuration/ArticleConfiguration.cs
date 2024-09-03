using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Repository.Data.Configuration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(A => A.Title).IsRequired();
            builder.Property(A => A.Content).IsRequired();
            builder.Property(A => A.Description).IsRequired();
            builder.Property(A => A.ImageUrl);
           
                     
        }
    }
}
