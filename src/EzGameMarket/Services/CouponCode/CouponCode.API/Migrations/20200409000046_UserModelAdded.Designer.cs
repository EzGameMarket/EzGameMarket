﻿// <auto-generated />
using System;
using CouponCode.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CouponCode.API.Migrations
{
    [DbContext(typeof(CouponCodeDbContext))]
    [Migration("20200409000046_UserModelAdded")]
    partial class UserModelAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CouponCode.API.Models.CouponCodeModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiscountID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLimitedForUsers")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("DiscountID");

                    b.ToTable("CouponCodes");
                });

            modelBuilder.Entity("CouponCode.API.Models.DiscountModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PercentageDiscount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("CouponCode.API.Models.EligibleUserModel", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CouponCodeModelID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CouponCodeModelID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CouponCode.API.Models.CouponCodeModel", b =>
                {
                    b.HasOne("CouponCode.API.Models.DiscountModel", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CouponCode.API.Models.EligibleUserModel", b =>
                {
                    b.HasOne("CouponCode.API.Models.CouponCodeModel", null)
                        .WithMany("Users")
                        .HasForeignKey("CouponCodeModelID");
                });
#pragma warning restore 612, 618
        }
    }
}
