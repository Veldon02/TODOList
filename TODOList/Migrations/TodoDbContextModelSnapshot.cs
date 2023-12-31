﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TODOList.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    partial class TodoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TODOList.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("description");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_completed");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 9, 25, 11, 29, 22, 956, DateTimeKind.Utc).AddTicks(5920),
                            IsCompleted = false,
                            Title = "Buy groceries"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 9, 25, 11, 29, 22, 956, DateTimeKind.Utc).AddTicks(5925),
                            IsCompleted = false,
                            Title = "Do laundry"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 9, 25, 11, 29, 22, 956, DateTimeKind.Utc).AddTicks(5927),
                            IsCompleted = false,
                            Title = "Finish project"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
