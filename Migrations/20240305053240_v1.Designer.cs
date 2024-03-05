﻿// <auto-generated />
using EnterpriceWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnterpriceWeb.Migrations
{
    [DbContext(typeof(AppDbConText))]
    [Migration("20240305053240_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EnterpriceWeb.Models.Article", b =>
                {
                    b.Property<int>("article_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("article_accept_date")
                        .HasColumnType("longtext");

                    b.Property<string>("article_avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("article_status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("article_submit_date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("article_title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("article_views")
                        .HasColumnType("longtext");

                    b.Property<int>("magazine_id")
                        .HasColumnType("int");

                    b.Property<int>("us_id")
                        .HasColumnType("int");

                    b.HasKey("article_id");

                    b.HasIndex("magazine_id");

                    b.HasIndex("us_id");

                    b.ToTable("articles");
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Article_file", b =>
                {
                    b.Property<int>("article_file_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("article_file_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("article_file_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("article_id")
                        .HasColumnType("int");

                    b.HasKey("article_file_id");

                    b.ToTable("article_Files");
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Faculty", b =>
                {
                    b.Property<int>("f_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("f_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("f_status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("f_id");

                    b.ToTable("faculties");

                    b.HasData(
                        new
                        {
                            f_id = 1,
                            f_name = "faculty1",
                            f_status = "1"
                        },
                        new
                        {
                            f_id = 2,
                            f_name = "faculty2",
                            f_status = "1"
                        },
                        new
                        {
                            f_id = 3,
                            f_name = "faculty3",
                            f_status = "1"
                        });
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Feedback", b =>
                {
                    b.Property<int>("fb_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("article_id")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("us_id")
                        .HasColumnType("int");

                    b.HasKey("fb_id");

                    b.HasIndex("article_id");

                    b.HasIndex("us_id");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Magazine", b =>
                {
                    b.Property<int>("magazine_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("magazine_academic_year")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("magazine_closure_date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("magazine_deleted")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("magazine_final_closure_date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("magazine_status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("magazine_title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("magazine_id");

                    b.ToTable("magazines");

                    b.HasData(
                        new
                        {
                            magazine_id = 1,
                            magazine_academic_year = "2019-2024",
                            magazine_closure_date = "03/02/2024",
                            magazine_deleted = "0",
                            magazine_final_closure_date = "27/03/2024",
                            magazine_status = "0",
                            magazine_title = "Tabloid"
                        });
                });

            modelBuilder.Entity("EnterpriceWeb.Models.User", b =>
                {
                    b.Property<int>("us_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("f_id")
                        .HasColumnType("int");

                    b.Property<string>("us_end_year")
                        .HasColumnType("longtext");

                    b.Property<string>("us_gender")
                        .HasColumnType("longtext");

                    b.Property<string>("us_gmail")
                        .HasColumnType("longtext");

                    b.Property<string>("us_image")
                        .HasColumnType("longtext");

                    b.Property<string>("us_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("us_password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("us_phone")
                        .HasColumnType("longtext");

                    b.Property<string>("us_role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("us_start_year")
                        .HasColumnType("longtext");

                    b.HasKey("us_id");

                    b.HasIndex("f_id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            us_id = 1,
                            f_id = 1,
                            us_name = "thaihuynh",
                            us_password = "huynhthai",
                            us_role = "admin"
                        });
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Article", b =>
                {
                    b.HasOne("EnterpriceWeb.Models.Magazine", "magazine")
                        .WithMany()
                        .HasForeignKey("magazine_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriceWeb.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("us_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("magazine");

                    b.Navigation("user");
                });

            modelBuilder.Entity("EnterpriceWeb.Models.Feedback", b =>
                {
                    b.HasOne("EnterpriceWeb.Models.Article", "Article")
                        .WithMany()
                        .HasForeignKey("article_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriceWeb.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("us_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EnterpriceWeb.Models.User", b =>
                {
                    b.HasOne("EnterpriceWeb.Models.Faculty", "faculty")
                        .WithMany()
                        .HasForeignKey("f_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("faculty");
                });
#pragma warning restore 612, 618
        }
    }
}
