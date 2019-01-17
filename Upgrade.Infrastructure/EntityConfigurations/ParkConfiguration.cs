using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class ParkConfiguration : EntityBaseConfiguration<Park>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Park> b)
        {
            b.Property(x => x.ParkId).HasMaxLength(200);
            b.Property(x => x.ParkName).HasMaxLength(100);
        }
    }
}
