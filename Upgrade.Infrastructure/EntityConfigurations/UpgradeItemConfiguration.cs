using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class UpgradeItemConfiguration : EntityBaseConfiguration<UpgradeItem>
    {
        public override void ConfigureDerived(EntityTypeBuilder<UpgradeItem> b)
        {
            b.Property(x => x.Creater).HasMaxLength(50);
            b.HasMany(x => x.ClientUpgradeItems).WithOne(x => x.UpgradeItem).HasForeignKey(d => d.UpgradeItemId);
            b.HasMany(x => x.UpgradeFiles).WithOne(x => x.UpgradeItem).HasForeignKey(d => d.UpgradeItemId);
            b.HasMany(x => x.ClientSets).WithOne(x => x.UpgradeItem).HasForeignKey(d => d.UpgradeItemId);
        }
    }
}
