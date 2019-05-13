using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Entities
{
    /// <summary>
    /// add-migration [name]
    /// update-database
    /// </summary>
    public class TemplateDbContext : DbContext
    {
        private const string CurrentTimeStampSql = "CURRENT_TIMESTAMP(6)";
        public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options){ }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(32);
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.Password).IsRequired().HasMaxLength(128);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Email).HasMaxLength(128);
                entity.Property(e => e.CreationTime).HasDefaultValueSql(CurrentTimeStampSql).ValueGeneratedOnAdd();
                entity.Property(e => e.LastUpdatedTime).HasDefaultValueSql(CurrentTimeStampSql).ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.CreationTime).HasDefaultValueSql(CurrentTimeStampSql).ValueGeneratedOnAdd();
                entity.Property(e => e.LastUpdatedTime).HasDefaultValueSql(CurrentTimeStampSql).ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
