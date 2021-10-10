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
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DistrictUserContext", b =>
                {
                    b.Property<int>("DisctrictsId")
                        .HasColumnType("int");

                    b.Property<int>("UserContextsId")
                        .HasColumnType("int");

                    b.HasKey("DisctrictsId", "UserContextsId");

                    b.HasIndex("UserContextsId");

                    b.ToTable("DistrictUserContext");
                });

            modelBuilder.Entity("Entities.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ЦАО"
                        },
                        new
                        {
                            Id = 2,
                            Name = "СВАО"
                        },
                        new
                        {
                            Id = 3,
                            Name = "СЗАО"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ЮАО"
                        },
                        new
                        {
                            Id = 5,
                            Name = "ЗАО"
                        },
                        new
                        {
                            Id = 6,
                            Name = "САО"
                        },
                        new
                        {
                            Id = 7,
                            Name = "ВАО"
                        },
                        new
                        {
                            Id = 8,
                            Name = "ЮВАО"
                        },
                        new
                        {
                            Id = 9,
                            Name = "ЮЗАО"
                        });
                });

            modelBuilder.Entity("Entities.Models.Flat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long>("CianId")
                        .HasColumnType("bigint");

                    b.Property<string>("CianReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Comission")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentFloor")
                        .HasColumnType("int");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<double>("FlatArea")
                        .HasColumnType("float");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxFloor")
                        .HasColumnType("int");

                    b.Property<string>("Metro")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("MoreThanYear")
                        .HasColumnType("bit");

                    b.Property<string>("PdfReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Pledge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<int?>("TimeToMetro")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WayToGo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Flats");
                });

            modelBuilder.Entity("Entities.Models.JobHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Message")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextFireAt")
                        .HasColumnType("datetime2(2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JobHistory");
                });

            modelBuilder.Entity("Entities.Models.NotificationContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastNotify")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("NextNotify")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("NotificationContext");
                });

            modelBuilder.Entity("Entities.Models.Proxy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ip")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Proxies");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Entities.Models.UserContext", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MaximumPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MinimumFloor")
                        .HasColumnType("int");

                    b.Property<decimal>("MinimumPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PostedNotifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomCountContext")
                        .HasColumnType("int");

                    b.Property<int>("TimeToMetro")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserContext");
                });

            modelBuilder.Entity("Entities.Models.UserState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("UserContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserContextId")
                        .IsUnique();

                    b.ToTable("UserState");
                });

            modelBuilder.Entity("DistrictUserContext", b =>
                {
                    b.HasOne("Entities.Models.District", null)
                        .WithMany()
                        .HasForeignKey("DisctrictsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.UserContext", null)
                        .WithMany()
                        .HasForeignKey("UserContextsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Flat", b =>
                {
                    b.HasOne("Entities.Models.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.Navigation("District");
                });

            modelBuilder.Entity("Entities.Models.NotificationContext", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithOne("NotificationContext")
                        .HasForeignKey("Entities.Models.NotificationContext", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.UserContext", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithOne("UserContext")
                        .HasForeignKey("Entities.Models.UserContext", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.UserState", b =>
                {
                    b.HasOne("Entities.Models.UserContext", "UserContext")
                        .WithOne("State")
                        .HasForeignKey("Entities.Models.UserState", "UserContextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserContext");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Navigation("NotificationContext");

                    b.Navigation("UserContext");
                });

            modelBuilder.Entity("Entities.Models.UserContext", b =>
                {
                    b.Navigation("State");
                });
#pragma warning restore 612, 618
        }
    }
}
