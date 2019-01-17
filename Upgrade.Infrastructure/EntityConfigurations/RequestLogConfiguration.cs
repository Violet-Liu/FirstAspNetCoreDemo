using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class RequestLogConfiguration : EntityBaseConfiguration<RequestLog>
    {
        public override void ConfigureDerived(EntityTypeBuilder<RequestLog> builder)
        {
            builder.Property(x => x.ParkId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.RespMsg).HasColumnType("TEXT");
            builder.Property(x => x.ParkIP).HasMaxLength(50);
            builder.Property(x => x.ActionName).HasColumnType("TEXT");
        }
    }
}
