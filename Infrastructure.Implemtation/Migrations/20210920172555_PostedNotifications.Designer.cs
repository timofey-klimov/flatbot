﻿// <auto-generated />
using System;
using Infrastructure.Implemtation.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Implemtation.Migrations
{
    [DbContext(typeof(FlatDbContext))]
    [Migration("20210920172555_PostedNotifications")]
    partial class PostedNotifications
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<double>("FlatArea")
                        .HasColumnType("float");

                    b.Property<int>("MaxFloor")
                        .HasColumnType("int");

                    b.Property<string>("Metro")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("MoreThanYear")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("Entities.Models.Metro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserContextId");

                    b.ToTable("Metro");
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

            modelBuilder.Entity("Entities.Models.Metro", b =>
                {
                    b.HasOne("Entities.Models.UserContext", null)
                        .WithMany("Metros")
                        .HasForeignKey("UserContextId");
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

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Navigation("NotificationContext");

                    b.Navigation("UserContext");
                });

            modelBuilder.Entity("Entities.Models.UserContext", b =>
                {
                    b.Navigation("Metros");
                });
#pragma warning restore 612, 618
        }
    }
}
