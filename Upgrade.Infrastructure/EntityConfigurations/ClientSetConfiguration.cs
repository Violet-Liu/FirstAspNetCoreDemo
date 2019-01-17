using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class ClientSetConfiguration : EntityBaseConfiguration<ClientSet>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ClientSet> b)
        {
            b.Property(x => x.ParkId).HasMaxLength(200);
            b.Property(x => x.ParkName).HasMaxLength(100);

        }
    }
}
