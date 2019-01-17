using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class ClientUpgradeItemConfiguration : EntityBaseConfiguration<ClientUpgradeItem>
    {
        public override void ConfigureDerived(EntityTypeBuilder<ClientUpgradeItem> b)
        {
            b.Property(x => x.ParkId).IsRequired().HasMaxLength(50);
            b.Property(x => x.IsUpgradeSucess).IsRequired();
            b.Property(x => x.Creater).HasMaxLength(50);
        }
    }
}
