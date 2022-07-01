﻿// <auto-generated />

using Domain.Context;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(ProjectAlfredContext))]
    [Migration("20220701071243_SongRecord_Remove_ID")]
    partial class SongRecord_Remove_ID
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("Domain.Entity.SongRecord", b =>
                {
                    b.Property<string>("OriginalUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalPoster")
                        .HasColumnType("TEXT");

                    b.HasKey("OriginalUrl", "OriginalPoster");

                    b.ToTable("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
