﻿// <auto-generated />
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DAL.Migrations
{
    [DbContext(typeof(ConfigDbContext))]
    partial class ConfigDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Common.Booking.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CarId");

                    b.Property<long>("ColorId");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(2048);

                    b.Property<DateTime?>("DateChangedUtc");

                    b.Property<DateTime>("DateCreatedUtc");

                    b.Property<long>("EngineId");

                    b.Property<long>("RimId");

                    b.Property<long>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ColorId");

                    b.HasIndex("EngineId");

                    b.HasIndex("RimId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DAL.Common.Booking.OrderAdditionalEquipmentItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("EquipmentId");

                    b.Property<Guid>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderAdditionalEquipmentItems");
                });

            modelBuilder.Entity("DAL.Common.Equipment.AdditionalEquipmentItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("AdditionalEquipmentItems");
                });

            modelBuilder.Entity("DAL.Common.Equipment.Car", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Price");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("DAL.Common.Equipment.Color", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Price");

                    b.Property<long>("TypeId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("DAL.Common.Equipment.Engine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<long>("FuelTypeId");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Power");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("FuelTypeId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("DAL.Common.Equipment.Rim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<decimal>("Diameter");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<decimal>("Price");

                    b.Property<long>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Rims");
                });

            modelBuilder.Entity("DAL.Common.Reference.ColorTypeRef", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("ColorTypes");
                });

            modelBuilder.Entity("DAL.Common.Reference.FuelTypeRef", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("DAL.Common.Reference.OrderStatusRef", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("DAL.Common.Reference.RimTypeRef", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("RimTypes");
                });

            modelBuilder.Entity("DAL.Common.Booking.Order", b =>
                {
                    b.HasOne("DAL.Common.Equipment.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Common.Equipment.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Common.Equipment.Engine", "Engine")
                        .WithMany()
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Common.Equipment.Rim", "Rim")
                        .WithMany()
                        .HasForeignKey("RimId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Common.Reference.OrderStatusRef", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Common.Booking.OrderAdditionalEquipmentItem", b =>
                {
                    b.HasOne("DAL.Common.Equipment.AdditionalEquipmentItem", "EquipmentItem")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DAL.Common.Booking.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Common.Equipment.Color", b =>
                {
                    b.HasOne("DAL.Common.Reference.ColorTypeRef", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Common.Equipment.Engine", b =>
                {
                    b.HasOne("DAL.Common.Reference.FuelTypeRef", "FuelType")
                        .WithMany()
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DAL.Common.Equipment.Rim", b =>
                {
                    b.HasOne("DAL.Common.Reference.RimTypeRef", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}