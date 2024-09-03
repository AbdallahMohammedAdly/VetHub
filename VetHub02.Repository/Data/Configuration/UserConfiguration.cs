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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(U => U.Name).IsRequired();
            builder.Property(U => U.Email).IsRequired();
            builder.Property(U => U.Gender).HasColumnType("bit");
            builder.Property(U => U.PhoneNumber).IsRequired();
            builder.Property(U => U.PhotoUrl).IsRequired();
       

        }
    }
}
