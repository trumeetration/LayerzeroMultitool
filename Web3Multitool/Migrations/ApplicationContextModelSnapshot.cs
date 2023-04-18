﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web3Multitool.Models;

#nullable disable

namespace Web3Multitool.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2");

            modelBuilder.Entity("Web3Multitool.Models.AccountInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ArbitrumInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AvaxInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CexAddress")
                        .HasColumnType("TEXT");

                    b.Property<int>("FantomInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OptimismInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PolygonInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalBalanceUsd")
                        .HasColumnType("REAL");

                    b.Property<int>("TotalTxAmount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArbitrumInfoId");

                    b.HasIndex("AvaxInfoId");

                    b.HasIndex("FantomInfoId");

                    b.HasIndex("OptimismInfoId");

                    b.HasIndex("PolygonInfoId");

                    b.ToTable("AccountInfos");
                });

            modelBuilder.Entity("Web3Multitool.Models.AddressChainInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BaseBalance")
                        .HasColumnType("REAL");

                    b.Property<int>("ChainId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FirstTxDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TxAmount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("UsdcBalance")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("AddressChainInfos");
                });

            modelBuilder.Entity("Web3Multitool.Models.AccountInfo", b =>
                {
                    b.HasOne("Web3Multitool.Models.AddressChainInfo", "ArbitrumInfo")
                        .WithMany()
                        .HasForeignKey("ArbitrumInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web3Multitool.Models.AddressChainInfo", "AvaxInfo")
                        .WithMany()
                        .HasForeignKey("AvaxInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web3Multitool.Models.AddressChainInfo", "FantomInfo")
                        .WithMany()
                        .HasForeignKey("FantomInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web3Multitool.Models.AddressChainInfo", "OptimismInfo")
                        .WithMany()
                        .HasForeignKey("OptimismInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web3Multitool.Models.AddressChainInfo", "PolygonInfo")
                        .WithMany()
                        .HasForeignKey("PolygonInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArbitrumInfo");

                    b.Navigation("AvaxInfo");

                    b.Navigation("FantomInfo");

                    b.Navigation("OptimismInfo");

                    b.Navigation("PolygonInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
