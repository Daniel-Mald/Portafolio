using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace PortafolioAPI.Models.Entities;

public partial class LabsystePortafolioContext : DbContext
{
    public LabsystePortafolioContext()
    {
    }

    public LabsystePortafolioContext(DbContextOptions<LabsystePortafolioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tag> Tag { get; set; }

    public virtual DbSet<Tagwork> Tagwork { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Work> Work { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tag");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<Tagwork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tagwork");

            entity.HasIndex(e => e.IdTag, "fk_tagwork_tag_idx");

            entity.HasIndex(e => e.IdWork, "fk_tagwork_work_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdTag).HasColumnType("int(11)");
            entity.Property(e => e.IdWork).HasColumnType("int(11)");

            entity.HasOne(d => d.IdTagNavigation).WithMany(p => p.Tagwork)
                .HasForeignKey(d => d.IdTag)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tagwork_tag");

            entity.HasOne(d => d.IdWorkNavigation).WithMany(p => p.Tagwork)
                .HasForeignKey(d => d.IdWork)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tagwork_work");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.Password).HasMaxLength(512);
            entity.Property(e => e.User).HasMaxLength(64);
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("work");

            entity.HasIndex(e => e.IdUser, "fk_work_user_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.IdUser).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(512);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Work)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_work_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
