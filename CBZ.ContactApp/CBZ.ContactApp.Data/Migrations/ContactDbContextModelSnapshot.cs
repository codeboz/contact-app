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
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Surname");

                    b.ToTable("Contact");
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
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ContactId", "InfoTypeId");

                    b.HasIndex("InfoTypeId");

                    b.ToTable("Info");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.InfoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Inserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("InfoType");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("ReportStateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Requested")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ReportStateId");

                    b.ToTable("ReportRequest");
                });

            modelBuilder.Entity("CBZ.ContactApp.Data.Model.ReportState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Inserted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ReportState");
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
                    b.HasOne("CBZ.ContactApp.Data.Model.ReportState", "ReportState")
                        .WithMany("ReportRequest")
                        .HasForeignKey("ReportStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
