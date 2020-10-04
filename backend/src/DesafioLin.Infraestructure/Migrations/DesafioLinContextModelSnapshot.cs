﻿// <auto-generated />
using System;
using DesafioLin.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioLin.Infraestructure.Migrations
{
    [DbContext(typeof(DesafioLinContext))]
    partial class DesafioLinContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("DesafioLin.Domain.Entities.Authorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<bool>("Value")
                        .HasColumnName("Value")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("authorization");
                });

            modelBuilder.Entity("DesafioLin.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .HasColumnName("Login")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .HasColumnName("Password")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("DesafioLin.Domain.Entities.Authorization", b =>
                {
                    b.HasOne("DesafioLin.Domain.Entities.User", "user")
                        .WithMany("authorizations")
                        .HasForeignKey("userId");
                });
#pragma warning restore 612, 618
        }
    }
}
