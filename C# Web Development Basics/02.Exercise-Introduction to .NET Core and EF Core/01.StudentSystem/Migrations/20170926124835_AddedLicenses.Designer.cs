﻿// <auto-generated />
using _01.StudentSystem.Data;
using _01.StudentSystem.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace _01.StudentSystem.Migrations
{
    [DbContext(typeof(StudentSystemDbContext))]
    [Migration("20170926124835_AddedLicenses")]
    partial class AddedLicenses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_01.StudentSystem.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.Property<DateTime>("SubmissionDate");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.License", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ResourceId");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ResourceType");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<DateTime>("RegistrationDate");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.StudentCourse", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("CourseId");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("_01.StudentSystem.Models.Homework", b =>
                {
                    b.HasOne("_01.StudentSystem.Models.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_01.StudentSystem.Models.Student", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_01.StudentSystem.Models.License", b =>
                {
                    b.HasOne("_01.StudentSystem.Models.Resource", "Resource")
                        .WithMany("Licenses")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_01.StudentSystem.Models.Resource", b =>
                {
                    b.HasOne("_01.StudentSystem.Models.Course", "Course")
                        .WithMany("Resources")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("_01.StudentSystem.Models.StudentCourse", b =>
                {
                    b.HasOne("_01.StudentSystem.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("_01.StudentSystem.Models.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
