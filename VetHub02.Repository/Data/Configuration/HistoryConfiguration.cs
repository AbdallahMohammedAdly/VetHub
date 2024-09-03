using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Repository.Data.Configuration
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.Property(H => H.ImageUrl).IsRequired();
            builder.Property(H => H.Symptoms).IsRequired();
            builder.Property(H => H.Treatment).IsRequired();
            builder.Property(H => H.Cause).IsRequired();
            builder.Property(H => H.PredictedName).IsRequired();
            builder.Property(H => H.Prevention).IsRequired();
            builder.Property(H => H.Transmission).IsRequired();
           
            
        }
    }
}
