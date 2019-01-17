﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Upgrade.Infrastructure.Data;

namespace Upgrade.Infrastructure.Migrations
{
    [DbContext(typeof(UDBContext))]
    [Migration("20181220072742_dropUpClientSuccessTime")]
    partial class dropUpClientSuccessTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Upgrade.Core.DomainModels.ClientUpgradeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Creater");

                    b.Property<bool>("IsUpgradeSucess");

                    b.Property<string>("ParkId");

                    b.Property<int>("UpgradeItemId");

                    b.HasKey("Id");

                    b.HasIndex("UpgradeItemId");

                    b.ToTable("ClientUpgradeItems");
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.RequestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("ParkIP");

                    b.Property<string>("ParkId");

                    b.Property<string>("RespMsg");

                    b.HasKey("Id");

                    b.ToTable("RequestLogs");
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.Software", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FilePathName");

                    b.Property<string>("SName");

                    b.Property<string>("SNumber");

                    b.HasKey("Id");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.UpgradeFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BucketName");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Creater");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<string>("Key");

                    b.Property<int>("UpgradeItemId");

                    b.HasKey("Id");

                    b.HasIndex("UpgradeItemId");

                    b.ToTable("UpgradeFiles");
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.UpgradeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Creater");

                    b.Property<bool>("IsValid");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UpgradeItems");
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.ClientUpgradeItem", b =>
                {
                    b.HasOne("Upgrade.Core.DomainModels.UpgradeItem", "UpgradeItem")
                        .WithMany("ClientUpgradeItems")
                        .HasForeignKey("UpgradeItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Upgrade.Core.DomainModels.UpgradeFiles", b =>
                {
                    b.HasOne("Upgrade.Core.DomainModels.UpgradeItem", "UpgradeItem")
                        .WithMany("UpgradeFiles")
                        .HasForeignKey("UpgradeItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}