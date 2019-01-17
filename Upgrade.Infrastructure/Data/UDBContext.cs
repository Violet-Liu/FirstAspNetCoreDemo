using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Upgrade.Core.DomainModels;

namespace Upgrade.Infrastructure.Data
{
    public class UDBContext : DbContext
    {
        public UDBContext(DbContextOptions<UDBContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.
        }

        public DbSet<ClientUpgradeItem> ClientUpgradeItems { get; set; }

        public DbSet<RequestLog> RequestLogs { get; set; }

        public DbSet<Software> Softwares { get; set; }

        public DbSet<UpgradeFiles> UpgradeFiles { get; set; }

        public DbSet<UpgradeItem> UpgradeItems { get; set; }

        public DbSet<Park> Parks { get; set; }

        public DbSet<ClientSet> ClientSets { get; set; }
    }
}
