﻿// <auto-generated />
using System;
using Event.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Event.DAL.Migrations
{
    [DbContext(typeof(EventDbContext))]
    [Migration("20230510194825_Add_Address_Field_To_Event")]
    partial class Add_Address_Field_To_Event
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Event.Domain.Entities.AccountEntity", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("account_id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(64)
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("token_created");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("token_expires");

                    b.HasKey("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("Event.Domain.Entities.EventEntity", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("event_id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("event_date");

                    b.Property<long>("Responses")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("responses");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("EventId");

                    b.HasIndex("AccountId");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("Event.Domain.Entities.RoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("RoleId");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Description = "Member",
                            Name = "Member"
                        },
                        new
                        {
                            RoleId = 2,
                            Description = "Organizer",
                            Name = "Organizer"
                        });
                });

            modelBuilder.Entity("accounts_to_events", b =>
                {
                    b.Property<Guid>("account_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("event_id")
                        .HasColumnType("uuid");

                    b.HasKey("account_id", "event_id");

                    b.HasIndex("event_id");

                    b.ToTable("accounts_to_events");
                });

            modelBuilder.Entity("Event.Domain.Entities.AccountEntity", b =>
                {
                    b.HasOne("Event.Domain.Entities.RoleEntity", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Event.Domain.Entities.EventEntity", b =>
                {
                    b.HasOne("Event.Domain.Entities.AccountEntity", "Account")
                        .WithMany("Events")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("accounts_to_events", b =>
                {
                    b.HasOne("Event.Domain.Entities.AccountEntity", null)
                        .WithMany()
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Event.Domain.Entities.EventEntity", null)
                        .WithMany()
                        .HasForeignKey("event_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Event.Domain.Entities.AccountEntity", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Event.Domain.Entities.RoleEntity", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
