﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClickSpace.DataAccess.Database
{
    public partial class ClickspaceOnlineshopContext : DbContext
    {
        public ClickspaceOnlineshopContext()
        {
        }

        public ClickspaceOnlineshopContext(DbContextOptions<ClickspaceOnlineshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartProduct> CartProduct { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductPicture> ProductPicture { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProduct_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartProduct)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProduct_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.PostDate).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PictureUrl).HasColumnName("PictureURL");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPicture)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductPicture_Product");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ProfilePictureUrl).HasColumnName("ProfilePictureURL");

                entity.Property(e => e.RegisterDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}