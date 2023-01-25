using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepositoryEF.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamePlatform> GamePlatforms { get; set; }

    public virtual DbSet<GamePublisher> GamePublishers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionSale> RegionSales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User Id=postgres;Password=C2WKIjQEdr4BsF5d;Server=db.blditmfikaiulhyinehk.supabase.co;Port=5432;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn" })
            .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
            .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "pgjwt")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("pgsodium", "pgsodium");

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_game");

            entity.ToTable("game");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GameName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("game_name");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.Games)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("fk_gm_gen");
        });

        modelBuilder.Entity<GamePlatform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_gameplatform");

            entity.ToTable("game_platform");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GamePublisherId).HasColumnName("game_publisher_id");
            entity.Property(e => e.PlatformId).HasColumnName("platform_id");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");

            entity.HasOne(d => d.GamePublishers).WithMany(p => p.GamePlatforms)
                .HasForeignKey(d => d.GamePublisherId)
                .HasConstraintName("fk_gpl_gp");

            entity.HasOne(d => d.Platform).WithMany(p => p.GamePlatforms)
                .HasForeignKey(d => d.PlatformId)
                .HasConstraintName("fk_gpl_pla");
        });

        modelBuilder.Entity<GamePublisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_gamepub");

            entity.ToTable("game_publisher");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

            entity.HasOne(d => d.Game).WithMany(p => p.GamePublishers)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("fk_gpu_gam");

            entity.HasOne(d => d.Publisher).WithMany(p => p.GamePublishers)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("fk_gpu_pub");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("platform_pkey");

            entity.ToTable("platform");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlatformName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("platform_name");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("publisher_pkey");

            entity.ToTable("publisher");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_pkey");

            entity.ToTable("region");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RegionName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("region_name");
        });

        modelBuilder.Entity<RegionSale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_region_sales");

            entity.ToTable("region_sales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GamePlatformId).HasColumnName("game_platform_id");
            entity.Property(e => e.NumSales)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("num_sales");
            entity.Property(e => e.RegionId).HasColumnName("region_id");

            entity.HasOne(d => d.GamePlatform).WithMany(p => p.RegionSales)
                .HasForeignKey(d => d.GamePlatformId)
                .HasConstraintName("fk_rs_gp");

            entity.HasOne(d => d.Region).WithMany(p => p.RegionSales)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("fk_rs_reg");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
