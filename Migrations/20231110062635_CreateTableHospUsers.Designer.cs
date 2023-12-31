﻿// <auto-generated />
using System;
using HospitalWebsiteApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospitalWebsiteApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231110062635_CreateTableHospUsers")]
    partial class CreateTableHospUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HospitalWebsiteApi.Models.UserParameters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ADDRESS");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("BIRTHDATE");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PASSWORD");

                    b.Property<int?>("PersonalID")
                        .HasColumnType("int")
                        .HasColumnName("PERSONALID");

                    b.Property<int?>("Phone")
                        .HasColumnType("int")
                        .HasColumnName("PHONE");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SURNAME");

                    b.Property<int>("UserRole")
                        .HasColumnType("int")
                        .HasColumnName("USER_ROLE");

                    b.HasKey("Id");

                    b.ToTable("HOSP_USERS", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
