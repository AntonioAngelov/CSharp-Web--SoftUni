﻿// <auto-generated />
using Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Application.Migrations
{
    [DbContext(typeof(JudgeDbContext))]
    [Migration("20171029123425_SucceededCOlumnToSubmissions")]
    partial class SucceededCOlumnToSubmissions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Application.Data.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("Application.Data.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<int>("ContestId");

                    b.Property<bool>("Succeeded");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FullName");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Data.Models.Contest", b =>
                {
                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany("Contests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Application.Data.Models.Submission", b =>
                {
                    b.HasOne("Application.Data.Models.Contest", "Contest")
                        .WithMany("Submissions")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
