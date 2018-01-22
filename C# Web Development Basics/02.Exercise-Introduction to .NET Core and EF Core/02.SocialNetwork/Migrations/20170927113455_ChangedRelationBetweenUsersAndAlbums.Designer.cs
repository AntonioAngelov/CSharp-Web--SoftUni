﻿// <auto-generated />
using _02.SocialNetwork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace _02.SocialNetwork.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    [Migration("20170927113455_ChangedRelationBetweenUsersAndAlbums")]
    partial class ChangedRelationBetweenUsersAndAlbums
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_02.SocialNetwork.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BackgroundColor");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.AlbumPicture", b =>
                {
                    b.Property<int>("AlbumId");

                    b.Property<int>("PictureId");

                    b.HasKey("AlbumId", "PictureId");

                    b.HasIndex("PictureId");

                    b.ToTable("AlbumPicture");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.AlbumTag", b =>
                {
                    b.Property<int>("AlbumId");

                    b.Property<int>("TagId");

                    b.HasKey("AlbumId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("AlbumTag");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Age");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool?>("IsDeleted");

                    b.Property<DateTime?>("LastTimeLoggedIn");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("ProfilePicture")
                        .HasMaxLength(1048576);

                    b.Property<DateTime?>("RegisteredOn");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.UserAlbum", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("AlbumId");

                    b.HasKey("UserId", "AlbumId");

                    b.HasIndex("AlbumId");

                    b.ToTable("UserAlbum");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.UserFriend", b =>
                {
                    b.Property<int>("FirstUserId");

                    b.Property<int>("SecondUserId");

                    b.HasKey("FirstUserId", "SecondUserId");

                    b.HasIndex("SecondUserId");

                    b.ToTable("UserFriend");
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.AlbumPicture", b =>
                {
                    b.HasOne("_02.SocialNetwork.Models.Album", "Album")
                        .WithMany("Pictures")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("_02.SocialNetwork.Models.Picture", "Picture")
                        .WithMany("Albums")
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.AlbumTag", b =>
                {
                    b.HasOne("_02.SocialNetwork.Models.Album", "Album")
                        .WithMany("Tags")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("_02.SocialNetwork.Models.Tag", "Tag")
                        .WithMany("Albums")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.UserAlbum", b =>
                {
                    b.HasOne("_02.SocialNetwork.Models.Album", "Album")
                        .WithMany("AlbumsHolder")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("_02.SocialNetwork.Models.User", "User")
                        .WithMany("Albums")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("_02.SocialNetwork.Models.UserFriend", b =>
                {
                    b.HasOne("_02.SocialNetwork.Models.User", "FirstUser")
                        .WithMany("Followers")
                        .HasForeignKey("FirstUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("_02.SocialNetwork.Models.User", "SecondUser")
                        .WithMany("Following")
                        .HasForeignKey("SecondUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
