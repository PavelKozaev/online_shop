﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240502103953_AddFavorites")]
    partial class AddFavorites
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favorites", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Author = "Сандроне Дациери",
                            Cost = 400m,
                            Description = "В парке в пригороде Рима исчез ребенок. Недалеко от места, где мальчика видели в последний раз, найдено тело его матери. Следователи подозревают главу семейства. Прибывшая на место преступления Коломба Каселли категорически не согласна с официальной версией. Однако ей приходится расследовать дело частным образом, ведь после трагических событий, связанных с гибелью людей в парижском ресторане, она готова подать в отставку...",
                            ImagePath = "/images/SD father.jpg",
                            Name = "Убить отца"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Author = "Сандроне Дациери",
                            Cost = 500m,
                            Description = "На вокзал Термини прибывает скоростной поезд «Милан-Рим», пассажиры расходятся, платформа пустеет, но из вагона класса «люкс» не выходит никто. Агент полиции Коломба Каселли, знакомая читателю по роману «Убить Отца», обнаруживает в вагоне тела людей, явно скончавшихся от удушья. Напрашивается версия о террористическом акте, которую готово подхватить руководство полиции. Однако Коломба подозревает, что дело вовсе не связано с террористами...",
                            ImagePath = "/images/SD angel.jpg",
                            Name = "Убить ангела"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            Author = "Сандроне Дациери",
                            Cost = 600m,
                            Description = "После ужасной снежной бури Коломба Каселли, знакомая читателям по романам «Убить Отца» и «Убить Ангела», обнаруживает в своем сарае подростка-аутиста по имени Томми, перемазанного кровью. Выясняется, что его родители убиты у него на глазах. И хотя она еще полтора года назад вышла в отставку, пытаясь оправиться после трагических событий в Венеции, когда она едва не погибла, а ее друг Данте Торре был похищен, молодая женщина вынуждена включиться в расследование...",
                            ImagePath = "/images/SD king.jpg",
                            Name = "Убить короля"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            Author = "Сандроне Дациери",
                            Cost = 700m,
                            Description = "Давным-давно Итала Карузо, полицейская «королева», управляющая разветвленной коррупционной сетью, была вынуждена посадить человека, которого ложно обвинили в том, что он похитил и убил трех девушек-подростков. Спустя тридцать лет случается новое похищение — и жертва, шестнадцатилетняя Амала, понимает, что вряд ли выберется живой, если не найдет способа сбежать самостоятельно. Амалу, впрочем, ищут. Ее ищет Франческа Кавальканте, сестра ее матери, адвокат...",
                            ImagePath = "/images/SD evil.jpg",
                            Name = "Зло, которе творят люди"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserDeliveryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserDeliveryInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("OnlineShop.Db.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favorites", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.UserDeliveryInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Favorites");
                });
#pragma warning restore 612, 618
        }
    }
}
