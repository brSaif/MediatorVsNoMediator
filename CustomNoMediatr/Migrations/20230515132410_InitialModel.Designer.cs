﻿// <auto-generated />

using CustomNoMediatr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CustomNoMediatr;

#nullable disable

namespace CustomNoMediatr.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20230515132410_InitialModel")]
    partial class InitialModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Services.Posts.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "some random body",
                            CreateBy = "foo",
                            Title = "PostOne"
                        },
                        new
                        {
                            Id = 2,
                            Body = "some random body",
                            CreateBy = "bar",
                            Title = "PostTwo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
