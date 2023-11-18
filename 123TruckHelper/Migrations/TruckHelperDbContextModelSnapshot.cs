﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _123TruckHelper.Models.EF;

#nullable disable

namespace _123TruckHelper.Migrations
{
    [DbContext(typeof(TruckHelperDbContext))]
    partial class TruckHelperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("_123TruckHelper.Models.EF.Load", b =>
                {
                    b.Property<int>("LoadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoadId"), 1L, 1);

                    b.Property<double>("DestinationLatitude")
                        .HasColumnType("float");

                    b.Property<double>("DestinationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("EquipmentType")
                        .HasColumnType("int");

                    b.Property<decimal>("Mileage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("OriginLatitude")
                        .HasColumnType("float");

                    b.Property<double>("OriginLongitude")
                        .HasColumnType("float");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LoadId");

                    b.ToTable("Loads");
                });

            modelBuilder.Entity("_123TruckHelper.Models.EF.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<int>("LoadId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("TruckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LoadId");

                    b.HasIndex("TruckId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("_123TruckHelper.Models.EF.Truck", b =>
                {
                    b.Property<int>("TruckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TruckId"), 1L, 1);

                    b.Property<bool>("Busy")
                        .HasColumnType("bit");

                    b.Property<int>("EquipType")
                        .HasColumnType("int");

                    b.Property<int>("NextTripLengthPreference")
                        .HasColumnType("int");

                    b.Property<double>("PositionLatitude")
                        .HasColumnType("float");

                    b.Property<double>("PositionLongitude")
                        .HasColumnType("float");

                    b.HasKey("TruckId");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("_123TruckHelper.Models.EF.Notification", b =>
                {
                    b.HasOne("_123TruckHelper.Models.EF.Load", "Load")
                        .WithMany()
                        .HasForeignKey("LoadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_123TruckHelper.Models.EF.Truck", "Truck")
                        .WithMany()
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Load");

                    b.Navigation("Truck");
                });
#pragma warning restore 612, 618
        }
    }
}
