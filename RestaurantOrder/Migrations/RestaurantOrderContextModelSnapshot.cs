﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestaurantOrder.Models;

namespace RestaurantOrder.Migrations
{
    [DbContext(typeof(RestaurantOrderContext))]
    partial class RestaurantOrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RestaurantOrder.Models.Dish", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<string>("TimeOfDay");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Dishes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 864, DateTimeKind.Local).AddTicks(7600),
                            Name = "eggs",
                            TimeOfDay = "morning",
                            Type = 1
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(770),
                            Name = "toast",
                            TimeOfDay = "morning",
                            Type = 2
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(790),
                            Name = "coffee",
                            TimeOfDay = "morning",
                            Type = 3
                        },
                        new
                        {
                            Id = 4L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800),
                            Name = "steak",
                            TimeOfDay = "night",
                            Type = 1
                        },
                        new
                        {
                            Id = 5L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800),
                            Name = "potato",
                            TimeOfDay = "night",
                            Type = 2
                        },
                        new
                        {
                            Id = 6L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(800),
                            Name = "wine",
                            TimeOfDay = "night",
                            Type = 3
                        },
                        new
                        {
                            Id = 7L,
                            CreatedAt = new DateTime(2019, 1, 5, 15, 42, 36, 874, DateTimeKind.Local).AddTicks(810),
                            Name = "cake",
                            TimeOfDay = "night",
                            Type = 4
                        });
                });

            modelBuilder.Entity("RestaurantOrder.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Input");

                    b.Property<string>("Output");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
