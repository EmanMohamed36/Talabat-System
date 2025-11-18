using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasColumnType("varchar(50)");
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar(50)");
            builder.Property(d => d.Description).HasColumnType("varchar(100)");

        }
    }
}
