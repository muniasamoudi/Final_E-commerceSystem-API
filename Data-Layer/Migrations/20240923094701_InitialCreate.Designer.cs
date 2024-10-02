﻿// <auto-generated />
using System;
using E_commerceSystem_API.Data_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Layer.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20240923094701_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data_Layer.DataModel.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Data_Layer.DataModel.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Data_Layer.DataModel.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Data_Layer.DataModel.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ownerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ownerId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts", (string)null);
                });

            modelBuilder.Entity("Data_Layer.DataModel.Order", b =>
                {
                    b.HasOne("Data_Layer.DataModel.Customer", "Customer")
                        .WithMany("orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Data_Layer.DataModel.Product", b =>
                {
                    b.HasOne("Data_Layer.DataModel.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderID");

                    b.HasOne("Data_Layer.DataModel.ShoppingCart", null)
                        .WithMany("products")
                        .HasForeignKey("ShoppingCartId");
                });

            modelBuilder.Entity("Data_Layer.DataModel.ShoppingCart", b =>
                {
                    b.HasOne("Data_Layer.DataModel.Customer", "owner")
                        .WithOne("Cart")
                        .HasForeignKey("Data_Layer.DataModel.ShoppingCart", "ownerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("owner");
                });

            modelBuilder.Entity("Data_Layer.DataModel.Customer", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("orders");
                });

            modelBuilder.Entity("Data_Layer.DataModel.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data_Layer.DataModel.ShoppingCart", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
