using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Templates.EntityFrameworkCore.Models;

namespace Templates.EntityFrameworkCore.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(32);
                entity.HasIndex(e => e.UserName).IsUnique(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Email).HasMaxLength(128);
            });
        }
    }
}
