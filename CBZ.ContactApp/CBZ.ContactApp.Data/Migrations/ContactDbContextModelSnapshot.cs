﻿// <auto-generated />
using System;
using CBZ.ContactApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CBZ.ContactApp.Data.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    partial class ContactDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Surname");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"),
                            Company = "A",
                            Name = "A",
                            Surname = "Aa"
                        },
                        new
                        {
                            Id = new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"),
                            Company = "B",
                            Name = "B",
                            Surname = "Bb"
                        },
                        new
                        {
                            Id = new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"),
                            Company = "C",
                            Name = "C",
                            Surname = "Cc"
                        },
                        new
                        {
                            Id = new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),
                            Company = "D",
                            Name = "D",
                            Surname = "Dd"
                        },
                        new
                        {
                            Id = new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"),
                            Company = "E",
                            Name = "E",
                            Surname = "Ee"
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.Info", b =>
                {
                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<int>("InfoTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("ContactId", "InfoTypeId");

                    b.HasIndex("InfoTypeId");

                    b.ToTable("Infos");

                    b.HasData(
                        new
                        {
                            ContactId = new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"),
                            InfoTypeId = 1,
                            Data = "11111111"
                        },
                        new
                        {
                            ContactId = new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"),
                            InfoTypeId = 2,
                            Data = "a@a.com"
                        },
                        new
                        {
                            ContactId = new Guid("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"),
                            InfoTypeId = 3,
                            Data = "Ankara"
                        },
                        new
                        {
                            ContactId = new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"),
                            InfoTypeId = 1,
                            Data = "222222222"
                        },
                        new
                        {
                            ContactId = new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"),
                            InfoTypeId = 2,
                            Data = "b@b.com"
                        },
                        new
                        {
                            ContactId = new Guid("3182b605-a8cb-4eb4-81fb-81129159f095"),
                            InfoTypeId = 3,
                            Data = "Ankara"
                        },
                        new
                        {
                            ContactId = new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"),
                            InfoTypeId = 1,
                            Data = "333333333"
                        },
                        new
                        {
                            ContactId = new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"),
                            InfoTypeId = 2,
                            Data = "c@c.com"
                        },
                        new
                        {
                            ContactId = new Guid("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"),
                            InfoTypeId = 3,
                            Data = "Ankara"
                        },
                        new
                        {
                            ContactId = new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),
                            InfoTypeId = 1,
                            Data = "4444444444"
                        },
                        new
                        {
                            ContactId = new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),
                            InfoTypeId = 2,
                            Data = "d@d.com"
                        },
                        new
                        {
                            ContactId = new Guid("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),
                            InfoTypeId = 3,
                            Data = "Bursa"
                        },
                        new
                        {
                            ContactId = new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"),
                            InfoTypeId = 1,
                            Data = "555555555"
                        },
                        new
                        {
                            ContactId = new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"),
                            InfoTypeId = 2,
                            Data = "e@e.com"
                        },
                        new
                        {
                            ContactId = new Guid("19c45392-88ba-4f96-92d4-c72024d48f81"),
                            InfoTypeId = 3,
                            Data = "Bursa"
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.InfoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn()
                        .HasIdentityOptions(100L, null, null, null, null, null);

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("InfoTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Phone"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Email"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Location"
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn()
                        .HasIdentityOptions(100L, null, null, null, null, null);

                    b.Property<int>("ContactCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhoneNumberCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactCount = 3,
                            Location = "Ankara",
                            PhoneNumberCount = 3
                        },
                        new
                        {
                            Id = 2,
                            ContactCount = 2,
                            Location = "Bursa",
                            PhoneNumberCount = 2
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("ReportId")
                        .HasColumnType("integer");

                    b.Property<int>("ReportStateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Requested")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.HasIndex("ReportStateId");

                    b.ToTable("ReportRequests");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bf5bd4b9-ebf5-41a8-a597-69b7aeb41432"),
                            Location = "Ankara",
                            ReportId = 1,
                            ReportStateId = 1,
                            Requested = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("4c802ae2-5292-4063-b5ca-e09b18871143"),
                            Location = "Bursa",
                            ReportId = 2,
                            ReportStateId = 2,
                            Requested = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn()
                        .HasIdentityOptions(100L, null, null, null, null, null);

                    b.Property<DateTime>("Inserted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ReportStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Preparing"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ready"
                        });
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.Info", b =>
                {
                    b.HasOne("CBZ.ContactApp.Data.Model.Contact", "Contact")
                        .WithMany("Infos")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBZ.ContactApp.Data.Model.InfoType", "InfoType")
                        .WithMany("Infos")
                        .HasForeignKey("InfoTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("InfoType");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportRequest", b =>
                {
                    b.HasOne("CBZ.ContactApp.Data.Model.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CBZ.ContactApp.Data.Model.ReportState", "ReportState")
                        .WithMany("ReportRequest")
                        .HasForeignKey("ReportStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");

                    b.Navigation("ReportState");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.Contact", b =>
                {
                    b.Navigation("Infos");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.InfoType", b =>
                {
                    b.Navigation("Infos");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportState", b =>
                {
                    b.Navigation("ReportRequest");
                });
#pragma warning restore 612, 618
        }
    }
}
