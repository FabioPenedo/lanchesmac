﻿// <auto-generated />
using System;
using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LanchesMac.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240708183934_PedidoDetalhes")]
    partial class PedidoDetalhes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LanchesMac.Models.CartPurchaseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CartPurchaseId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("SnackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SnackId");

                    b.ToTable("CartPurchaseItens");
                });

            modelBuilder.Entity("LanchesMac.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LanchesMac.Models.DetailOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SnackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SnackId");

                    b.ToTable("DetailsOrder");
                });

            modelBuilder.Entity("LanchesMac.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("OrderDeliveredTo")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("OrderDispatched")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<int>("TotalItemsOrdered")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalOrder")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LanchesMac.Models.Snack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageThumbUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("InStock")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSnackFavorite")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Snacks");
                });

            modelBuilder.Entity("LanchesMac.Models.CartPurchaseItem", b =>
                {
                    b.HasOne("LanchesMac.Models.Snack", "Snack")
                        .WithMany()
                        .HasForeignKey("SnackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snack");
                });

            modelBuilder.Entity("LanchesMac.Models.DetailOrder", b =>
                {
                    b.HasOne("LanchesMac.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LanchesMac.Models.Snack", "Snack")
                        .WithMany()
                        .HasForeignKey("SnackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Snack");
                });

            modelBuilder.Entity("LanchesMac.Models.Snack", b =>
                {
                    b.HasOne("LanchesMac.Models.Category", "Category")
                        .WithMany("Snacks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LanchesMac.Models.Category", b =>
                {
                    b.Navigation("Snacks");
                });

            modelBuilder.Entity("LanchesMac.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}