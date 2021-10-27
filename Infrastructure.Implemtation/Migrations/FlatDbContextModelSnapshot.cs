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
                    b.Property<int>("DistrictsId")
                        .HasColumnType("int");

                    b.Property<int>("UserContextsId")
                        .HasColumnType("int");

                    b.HasKey("DistrictsId", "UserContextsId");

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

            modelBuilder.Entity("Entities.Models.FlatEntities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OkrugId")
                        .HasColumnType("int");

                    b.Property<string>("Raion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.HasIndex("OkrugId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.BuildingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuildYear")
                        .HasColumnType("int");

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.ToTable("BuildingInfo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.Flat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("CellingHeight")
                        .HasColumnType("float");

                    b.Property<long>("CianId")
                        .HasColumnType("bigint");

                    b.Property<string>("CianUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(0)")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CurrentFloor")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorsCount")
                        .HasColumnType("int");

                    b.Property<bool?>("HasFurniture")
                        .HasColumnType("bit");

                    b.Property<string>("LeaseTermType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomsCount")
                        .HasColumnType("int");

                    b.Property<double?>("TotalArea")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Flats");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.FlatGeo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.ToTable("FlatGeo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.ParkingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsFree")
                        .HasColumnType("bit");

                    b.Property<string>("ParkingType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriceMonthly")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.ToTable("ParkingInfo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.PhotoInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.ToTable("PhotoInfo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.PriceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AgentFee")
                        .HasColumnType("int");

                    b.Property<int?>("Deposit")
                        .HasColumnType("int");

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlatId")
                        .IsUnique();

                    b.ToTable("PriceInfo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.RailwayInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.ToTable("RailwayInfo");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.UndergroundInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FlatId");

                    b.ToTable("UndergroundInfo");
                });

            modelBuilder.Entity("Entities.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CiandId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Entities.Models.JobHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("FinishTime")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SheduleJobManagerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SheduleJobManagerId");

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

            modelBuilder.Entity("Entities.Models.SheduleJobManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRunning")
                        .HasColumnType("bit");

                    b.Property<string>("JobManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobType")
                        .HasColumnType("int");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PlanningRunTime")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("RunTime")
                        .HasColumnType("datetime2(0)");

                    b.HasKey("Id");

                    b.ToTable("SheduleJobManagers");

                    b.HasDiscriminator<int>("JobType");
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

            modelBuilder.Entity("Entities.Models.UserAgregate.PostedNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CianId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserContextId");

                    b.ToTable("PostedNotification");
                });

            modelBuilder.Entity("Entities.Models.UserAgregate.UserRoomCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<int>("UserContextId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserContextId");

                    b.ToTable("UserRoomCount");
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

            modelBuilder.Entity("Entities.Models.ReccurentJobManager", b =>
                {
                    b.HasBaseType("Entities.Models.SheduleJobManager");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Entities.Models.TimedJobManager", b =>
                {
                    b.HasBaseType("Entities.Models.SheduleJobManager");

                    b.Property<DateTime>("SheduleRunTime")
                        .HasColumnType("datetime2(0)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("DistrictUserContext", b =>
                {
                    b.HasOne("Entities.Models.District", null)
                        .WithMany()
                        .HasForeignKey("DistrictsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.UserContext", null)
                        .WithMany()
                        .HasForeignKey("UserContextsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.Address", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithOne("Address")
                        .HasForeignKey("Entities.Models.FlatEntities.Address", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.District", "Okrug")
                        .WithMany()
                        .HasForeignKey("OkrugId");

                    b.Navigation("Okrug");
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.BuildingInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithOne("BuildingInfo")
                        .HasForeignKey("Entities.Models.FlatEntities.BuildingInfo", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.FlatGeo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithOne("FlatGeo")
                        .HasForeignKey("Entities.Models.FlatEntities.FlatGeo", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.ParkingInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithOne("ParkingInfo")
                        .HasForeignKey("Entities.Models.FlatEntities.ParkingInfo", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.Phone", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithMany("Phones")
                        .HasForeignKey("FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.PhotoInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithMany("PhotoInfos")
                        .HasForeignKey("FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.PriceInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithOne("PriceInfo")
                        .HasForeignKey("Entities.Models.FlatEntities.PriceInfo", "FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.RailwayInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithMany("RailwayInfos")
                        .HasForeignKey("FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.FlatEntities.UndergroundInfo", b =>
                {
                    b.HasOne("Entities.Models.FlatEntities.Flat", null)
                        .WithMany("UndergroundInfos")
                        .HasForeignKey("FlatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.JobHistory", b =>
                {
                    b.HasOne("Entities.Models.SheduleJobManager", null)
                        .WithMany("JobHistories")
                        .HasForeignKey("SheduleJobManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.NotificationContext", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithOne("NotificationContext")
                        .HasForeignKey("Entities.Models.NotificationContext", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.UserAgregate.PostedNotification", b =>
                {
                    b.HasOne("Entities.Models.UserContext", null)
                        .WithMany("PostedNotifications")
                        .HasForeignKey("UserContextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.UserAgregate.UserRoomCount", b =>
                {
                    b.HasOne("Entities.Models.UserContext", null)
                        .WithMany("UserRoomCounts")
                        .HasForeignKey("UserContextId")
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

            modelBuilder.Entity("Entities.Models.FlatEntities.Flat", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("BuildingInfo");

                    b.Navigation("FlatGeo");

                    b.Navigation("ParkingInfo");

                    b.Navigation("Phones");

                    b.Navigation("PhotoInfos");

                    b.Navigation("PriceInfo");

                    b.Navigation("RailwayInfos");

                    b.Navigation("UndergroundInfos");
                });

            modelBuilder.Entity("Entities.Models.SheduleJobManager", b =>
                {
                    b.Navigation("JobHistories");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Navigation("NotificationContext");

                    b.Navigation("UserContext");
                });

            modelBuilder.Entity("Entities.Models.UserContext", b =>
                {
                    b.Navigation("PostedNotifications");

                    b.Navigation("State");

                    b.Navigation("UserRoomCounts");
                });
#pragma warning restore 612, 618
        }
    }
}
