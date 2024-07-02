﻿// <auto-generated />
using System;
using EventMenagementSystem.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventMenagementSystem.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240619080013_JWTCode")]
    partial class JWTCode
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventManagementSystem.Model.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganizerID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("OrganizerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventManagementSystem.Model.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EventID")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EventManagementSystem.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JwtToken")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventManagementSystem.Model.UserEvent", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnOrder(0);

                    b.Property<int>("EventID")
                        .HasColumnType("integer")
                        .HasColumnOrder(1);

                    b.HasKey("UserID", "EventID");

                    b.HasIndex("EventID");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("EventManagementSystem.Model.Event", b =>
                {
                    b.HasOne("EventManagementSystem.Model.User", "Organizer")
                        .WithMany("OrganizedEvents")
                        .HasForeignKey("OrganizerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("EventManagementSystem.Model.Review", b =>
                {
                    b.HasOne("EventManagementSystem.Model.Event", "Event")
                        .WithMany("Reviews")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagementSystem.Model.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventManagementSystem.Model.UserEvent", b =>
                {
                    b.HasOne("EventManagementSystem.Model.Event", "Event")
                        .WithMany("UserEvents")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventManagementSystem.Model.User", "User")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventManagementSystem.Model.Event", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("UserEvents");
                });

            modelBuilder.Entity("EventManagementSystem.Model.User", b =>
                {
                    b.Navigation("OrganizedEvents");

                    b.Navigation("Reviews");

                    b.Navigation("UserEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
