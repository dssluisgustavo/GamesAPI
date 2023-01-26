﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RepositoryEF.Models;

#nullable disable

namespace RepositoryEF.Migrations
{
    [DbContext(typeof(PostgresContext))]
    partial class PostgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RepositoryEF.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GameName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("game_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<int?>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.HasKey("Id")
                        .HasName("pk_game");

                    b.HasIndex("GenreId");

                    b.ToTable("game", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GamePublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("game_publisher_id");

                    b.Property<int?>("PlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("platform_id");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("integer")
                        .HasColumnName("release_year");

                    b.HasKey("Id")
                        .HasName("pk_gameplatform");

                    b.HasIndex("GamePublisherId");

                    b.HasIndex("PlatformId");

                    b.ToTable("game_platform", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("publisher_id");

                    b.HasKey("Id")
                        .HasName("pk_gamepub");

                    b.HasIndex("GameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("game_publisher", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GenreName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("genre_pkey");

                    b.ToTable("genre", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PlatformName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("platform_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("platform_pkey");

                    b.ToTable("platform", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PublisherName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("publisher_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("publisher_pkey");

                    b.ToTable("publisher", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RegionName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("region_name")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("region_pkey");

                    b.ToTable("region", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.RegionSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GamePlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("game_platform_id");

                    b.Property<decimal?>("NumSales")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("num_sales")
                        .HasDefaultValueSql("NULL::numeric");

                    b.Property<int?>("RegionId")
                        .HasColumnType("integer")
                        .HasColumnName("region_id");

                    b.HasKey("Id")
                        .HasName("pk_region_sales");

                    b.HasIndex("GamePlatformId");

                    b.HasIndex("RegionId");

                    b.ToTable("region_sales", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("password")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<string>("Username")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("username")
                        .HasDefaultValueSql("NULL::character varying");

                    b.HasKey("Id")
                        .HasName("user_pk");

                    b.ToTable("user", "public");
                });

            modelBuilder.Entity("RepositoryEF.Models.Game", b =>
                {
                    b.HasOne("RepositoryEF.Models.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("fk_gm_gen");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePlatform", b =>
                {
                    b.HasOne("RepositoryEF.Models.GamePublisher", "GamePublishers")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GamePublisherId")
                        .HasConstraintName("fk_gpl_gp");

                    b.HasOne("RepositoryEF.Models.Platform", "Platform")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformId")
                        .HasConstraintName("fk_gpl_pla");

                    b.Navigation("GamePublishers");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePublisher", b =>
                {
                    b.HasOne("RepositoryEF.Models.Game", "Game")
                        .WithMany("GamePublishers")
                        .HasForeignKey("GameId")
                        .HasConstraintName("fk_gpu_gam");

                    b.HasOne("RepositoryEF.Models.Publisher", "Publisher")
                        .WithMany("GamePublishers")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("fk_gpu_pub");

                    b.Navigation("Game");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("RepositoryEF.Models.RegionSale", b =>
                {
                    b.HasOne("RepositoryEF.Models.GamePlatform", "GamePlatform")
                        .WithMany("RegionSales")
                        .HasForeignKey("GamePlatformId")
                        .HasConstraintName("fk_rs_gp");

                    b.HasOne("RepositoryEF.Models.Region", "Region")
                        .WithMany("RegionSales")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("fk_rs_reg");

                    b.Navigation("GamePlatform");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("RepositoryEF.Models.Game", b =>
                {
                    b.Navigation("GamePublishers");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePlatform", b =>
                {
                    b.Navigation("RegionSales");
                });

            modelBuilder.Entity("RepositoryEF.Models.GamePublisher", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("RepositoryEF.Models.Genre", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("RepositoryEF.Models.Platform", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("RepositoryEF.Models.Publisher", b =>
                {
                    b.Navigation("GamePublishers");
                });

            modelBuilder.Entity("RepositoryEF.Models.Region", b =>
                {
                    b.Navigation("RegionSales");
                });
#pragma warning restore 612, 618
        }
    }
}