﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShoppyMcShopFace.Models;

#nullable disable

namespace ShoppyMcShopFace.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShoppyMcShopFace.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InvoiceId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("InvoiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.InvoiceLineItems", b =>
                {
                    b.Property<int>("LineItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LineItemId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int?>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("LineItemId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceLineItems");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateFulfilled")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateOrdered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("OrderingUserUserId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentMethodUsedPaymentMethodId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderingUserUserId");

                    b.HasIndex("PaymentMethodUsedPaymentMethodId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MethodNickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("CreatingUserUserId")
                        .HasColumnType("integer");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("ProductStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<string>("TagsJSON")
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProductId");

                    b.HasIndex("CreatingUserUserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.ProductInOrder", b =>
                {
                    b.Property<int>("ProductInOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductInOrderId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProductInOrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsInOrders");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserLevel")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Invoice", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.User", "InvoicedUser")
                        .WithMany("InvoicesOfUser")
                        .HasForeignKey("UserId");

                    b.Navigation("InvoicedUser");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.InvoiceLineItems", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.Invoice", null)
                        .WithMany("ItemsInInvoice")
                        .HasForeignKey("InvoiceId");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Order", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.User", "OrderingUser")
                        .WithMany("OrdersUserPlaced")
                        .HasForeignKey("OrderingUserUserId");

                    b.HasOne("ShoppyMcShopFace.Models.PaymentMethod", "PaymentMethodUsed")
                        .WithMany("OrdersPaidFor")
                        .HasForeignKey("PaymentMethodUsedPaymentMethodId");

                    b.HasOne("ShoppyMcShopFace.Models.User", null)
                        .WithOne("ShoppingCart")
                        .HasForeignKey("ShoppyMcShopFace.Models.Order", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("OrderingUser");

                    b.Navigation("PaymentMethodUsed");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.PaymentMethod", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.User", "PaymentMethodOwner")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("PaymentMethodOwner");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Product", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.User", "CreatingUser")
                        .WithMany()
                        .HasForeignKey("CreatingUserUserId");

                    b.Navigation("CreatingUser");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.ProductInOrder", b =>
                {
                    b.HasOne("ShoppyMcShopFace.Models.Order", "OrderBelongedTo")
                        .WithMany("ProductsInOrder")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppyMcShopFace.Models.Product", "OrderedProduct")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderBelongedTo");

                    b.Navigation("OrderedProduct");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Invoice", b =>
                {
                    b.Navigation("ItemsInInvoice");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.Order", b =>
                {
                    b.Navigation("ProductsInOrder");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.PaymentMethod", b =>
                {
                    b.Navigation("OrdersPaidFor");
                });

            modelBuilder.Entity("ShoppyMcShopFace.Models.User", b =>
                {
                    b.Navigation("InvoicesOfUser");

                    b.Navigation("OrdersUserPlaced");

                    b.Navigation("ShoppingCart")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
