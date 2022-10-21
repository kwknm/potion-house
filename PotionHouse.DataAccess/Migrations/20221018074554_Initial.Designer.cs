﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PotionHouse.DataAccess;

#nullable disable

namespace PotionHouse.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221018074554_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("PotionHouse.DataAccess.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PotionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PotionId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("PotionHouse.DataAccess.Entities.Potion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PreparationCost")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("PreparationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Potions");
                });

            modelBuilder.Entity("PotionHouse.DataAccess.Entities.Ingredient", b =>
                {
                    b.HasOne("PotionHouse.DataAccess.Entities.Potion", null)
                        .WithMany("Recipe")
                        .HasForeignKey("PotionId");
                });

            modelBuilder.Entity("PotionHouse.DataAccess.Entities.Potion", b =>
                {
                    b.Navigation("Recipe");
                });
#pragma warning restore 612, 618
        }
    }
}
