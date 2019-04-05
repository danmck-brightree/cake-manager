﻿// <auto-generated />
using System;
using CakeManager.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CakeManager.Server.Migrations
{
    [DbContext(typeof(CakeMarkDbContext))]
    [Migration("20190312140206_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("7357d973-d996-4c94-8806-a6264d158af6"),
                            Name = "Aberdeen"
                        },
                        new
                        {
                            Id = new Guid("c4b38f56-a424-41ea-94ed-238131ecf415"),
                            Name = "Glasgow"
                        });
                });

            modelBuilder.Entity("CakeManager.Repository.Models.SuperCakeMark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SuperCakeMark");
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
                            OfficeId = new Guid("7357d973-d996-4c94-8806-a6264d158af6"),
                            Password = "temp"
                        },
                        new
                        {
                            Id = new Guid("9cf309c5-aee8-4952-be05-d377d774875e"),
                            Email = "jsmith@brightree.com",
                            Name = "John Smith",
                            OfficeId = new Guid("7357d973-d996-4c94-8806-a6264d158af6"),
                            Password = "temp"
                        },
                        new
                        {
                            Id = new Guid("2fe364cb-e700-4c04-90ee-87908bd2f259"),
                            Email = "tperson@brightree.com",
                            Name = "Test Person",
                            OfficeId = new Guid("c4b38f56-a424-41ea-94ed-238131ecf415"),
                            Password = "temp"
                        });
                });

            modelBuilder.Entity("CakeManager.Repository.Models.CakeMark", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.TempUser", "User")
                        .WithMany("CakeMarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CakeManager.Repository.Models.SuperCakeMark", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.TempUser", "User")
                        .WithMany("SuperCakeMarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CakeManager.Repository.Models.TempUser", b =>
                {
                    b.HasOne("CakeManager.Repository.Models.Office", "Office")
                        .WithMany("Users")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}