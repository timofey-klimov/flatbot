// <auto-generated />
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
    [Migration("20210911105121_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long>("CianId")
                        .HasColumnType("bigint");

                    b.Property<string>("CianReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Comission")
                        .HasColumnType("int");

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

                    b.Property<bool?>("NeedComission")
                        .HasColumnType("bit");

                    b.Property<bool?>("NeedPledge")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Pledge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomCount")
                        .HasColumnType("int");

                    b.Property<int?>("TimeToMetro")
                        .HasColumnType("int");

                    b.Property<int?>("WayToGo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Flats");
                });
#pragma warning restore 612, 618
        }
    }
}
