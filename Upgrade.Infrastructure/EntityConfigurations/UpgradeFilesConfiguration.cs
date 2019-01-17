using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class UpgradeFilesConfiguration : EntityBaseConfiguration<UpgradeFiles>
    {
        public override void ConfigureDerived(EntityTypeBuilder<UpgradeFiles> b)
        {
            b.Property(x => x.FileName).IsRequired().HasMaxLength(200);
            b.Property(x => x.Creater).HasMaxLength(50);
        }
    }
}
