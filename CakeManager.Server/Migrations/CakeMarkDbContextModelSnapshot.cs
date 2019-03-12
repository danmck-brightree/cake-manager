﻿// <auto-generated />
using System;
using CakeManager.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CakeManager.Server.Migrations
{
    [DbContext(typeof(CakeMarkDbContext))]
    partial class CakeMarkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview3.19153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CakeManager.Repository.Models.CakeMark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CakeMark");
                });

            modelBuilder.Entity("CakeManager.Repository.Models.Office", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Office");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"),
                            Name = "Aberdeen"
                        },
                        new
                        {
                            Id = new Guid("31cf2f00-69a8-4a6a-90b4-2f34b907bf05"),
                            Name = "Glasgow"
                        });
                });

            modelBuilder.Entity("CakeManager.Repository.Models.TempUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("OfficeId");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("TempUser");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2234a4e3-7ae6-4882-b204-81f38a3d7d47"),
                            Email = "dmckenzie@brightree.com",
                            Name = "Daniel McKenzie",
                            OfficeId = new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"),
                            Password = "temp"
                        },
                        new
                        {
                            Id = new Guid("2f24cae6-02da-48e6-b933-d4ac6849310b"),
                            Email = "jsmith@brightree.com",
                            Name = "John Smith",
                            OfficeId = new Guid("dfc0c273-3e77-4d27-a29a-c400ed740431"),
                            Password = "temp"
                        },
                        new
                        {
                            Id = new Guid("ec626486-ccf2-4ef7-9eca-42c81d6de4a9"),
                            Email = "tperson@brightree.com",
                            Name = "Test Person",
                            OfficeId = new Guid("31cf2f00-69a8-4a6a-90b4-2f34b907bf05"),
                            Password = "temp"
                        });
                });

            modelBuilder.Entity("CakeManager.Repository.Models.TempUserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TempUserToken");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dbeac247-b2ef-4dc8-be09-0f5370596ecf"),
                            Token = "06a0d9bf-6932-4807-a672-a88d23b37f78",
                            UserId = new Guid("2234a4e3-7ae6-4882-b204-81f38a3d7d47")
                        });
                });

            modelBuilder.Entity("CakeManager.Repository.Models.CakeMark", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.TempUser", "User")
                        .WithMany("CakeMarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CakeManager.Repository.Models.TempUser", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CakeManager.Repository.Models.TempUserToken", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.TempUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}