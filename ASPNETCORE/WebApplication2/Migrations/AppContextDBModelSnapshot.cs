﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication2.Controllers;

namespace WebApplication2.Migrations
{
    [DbContext(typeof(AppContextDB))]
    partial class AppContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("WebApplication2.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ИТ"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Программисты"
                        });
                });

            modelBuilder.Entity("WebApplication2.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentId = 2,
                            Email = "ivanov@mail.ru",
                            Name = "Иванов",
                            PhoneNumber = 123L
                        },
                        new
                        {
                            Id = 2,
                            DepartmentId = 1,
                            Email = "petrov@mail.ru",
                            Name = "Петров",
                            PhoneNumber = 456L
                        },
                        new
                        {
                            Id = 3,
                            DepartmentId = 2,
                            Email = "sidrov@mail.ru",
                            Name = "Сидоров",
                            PhoneNumber = 789L
                        });
                });

            modelBuilder.Entity("WebApplication2.Models.User", b =>
                {
                    b.HasOne("WebApplication2.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });
#pragma warning restore 612, 618
        }
    }
}