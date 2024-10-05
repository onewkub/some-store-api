﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SomeStoreAPI.Data;

#nullable disable

namespace SomeStoreAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("ProductCategories", (string)null);
                });

            modelBuilder.Entity("ProductTag", b =>
                {
                    b.Property<int>("ProductsProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductsProductId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ProductTags", (string)null);
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Alt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Large")
                        .HasColumnType("TEXT");

                    b.Property<string>("Medium")
                        .HasColumnType("TEXT");

                    b.Property<string>("MediumLarge")
                        .HasColumnType("TEXT");

                    b.Property<string>("MediumSmall")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Original")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Small")
                        .HasColumnType("TEXT");

                    b.Property<string>("SmallThumbnail")
                        .HasColumnType("TEXT");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllowMultipleConfigs")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReviewCount")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("ReviewScore")
                        .HasColumnType("REAL");

                    b.Property<int>("Sold")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ThumbnailImage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CollectionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CollectionName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ThumbnailImage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserIdentifier")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("SomeStoreAPI.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SomeStoreAPI.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductTag", b =>
                {
                    b.HasOne("SomeStoreAPI.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SomeStoreAPI.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Image", b =>
                {
                    b.HasOne("SomeStoreAPI.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Product", b =>
                {
                    b.OwnsOne("SomeStoreAPI.Models.Brand", "Brand", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.Property<string>("UserIdentifier")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("SomeStoreAPI.Models.Price", "FullPriceBeforeOverallDiscount", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal?>("Amount")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("SomeStoreAPI.Models.Price", "OriginalPrice", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal?>("Amount")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("SomeStoreAPI.Models.Price", "PossibleDiscountPrice", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal?>("Amount")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("SomeStoreAPI.Models.Price", "Price", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal?>("Amount")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Currency")
                                .HasColumnType("TEXT");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Brand")
                        .IsRequired();

                    b.Navigation("FullPriceBeforeOverallDiscount")
                        .IsRequired();

                    b.Navigation("OriginalPrice")
                        .IsRequired();

                    b.Navigation("PossibleDiscountPrice")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("SomeStoreAPI.Models.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
