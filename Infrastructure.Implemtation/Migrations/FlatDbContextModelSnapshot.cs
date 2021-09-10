﻿// <auto-generated />
using System;
using Infrastructure.Implemtation.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Implemtation.Migrations
{
    [DbContext(typeof(FlatDbContext))]
    partial class FlatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Flat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("CeilingHeight")
                        .HasColumnType("float");

                    b.Property<long>("CianId")
                        .HasColumnType("bigint");

                    b.Property<int>("CurrentFloor")
                        .HasColumnType("int");

                    b.Property<int>("LastFloor")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RoomArea")
                        .HasColumnType("float");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Flats");
                });

            modelBuilder.Entity("Entities.Models.Flat", b =>
                {
                    b.OwnsOne("Entities.Models.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("FlatId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Value")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Address");

                            b1.HasKey("FlatId");

                            b1.ToTable("Flats");

                            b1.WithOwner()
                                .HasForeignKey("FlatId");
                        });

                    b.OwnsOne("Entities.Models.ValueObjects.Metro", "Metro", b1 =>
                        {
                            b1.Property<int>("FlatId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Name")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("MetroName");

                            b1.Property<bool>("OnCar")
                                .HasColumnType("bit")
                                .HasColumnName("OnCar");

                            b1.Property<int?>("TimeToGoInMinutes")
                                .HasColumnType("int")
                                .HasColumnName("TimeToGo");

                            b1.HasKey("FlatId");

                            b1.ToTable("Flats");

                            b1.WithOwner()
                                .HasForeignKey("FlatId");
                        });

                    b.OwnsOne("Entities.Models.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<int>("FlatId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int?>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Price");

                            b1.HasKey("FlatId");

                            b1.ToTable("Flats");

                            b1.WithOwner()
                                .HasForeignKey("FlatId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Metro");

                    b.Navigation("Price");
                });
#pragma warning restore 612, 618
        }
    }
}