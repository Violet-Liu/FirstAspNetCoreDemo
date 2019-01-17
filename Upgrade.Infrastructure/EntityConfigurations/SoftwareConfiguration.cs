using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.EntityConfigurations
{
    public class SoftwareConfiguration : EntityBaseConfiguration<Software>
    {
        public override void ConfigureDerived(EntityTypeBuilder<Software> b)
        {
            b.Property(x => x.SName).IsRequired().HasMaxLength(50);
            b.Property(x => x.SNumber).IsRequired().HasMaxLength(50);
        }
    }
}
