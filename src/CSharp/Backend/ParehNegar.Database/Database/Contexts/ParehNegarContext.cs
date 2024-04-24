using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Database.Entities;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParehNegar.Database.Entities.Authentications;

namespace ParehNegar.Database.Contexts;

public sealed class ParehNegarContext : DbContext
{
    private DatabaseBuilder _builder;
    public ParehNegarContext(DatabaseBuilder builder) 
    {
            _builder = builder;
    }

    // Contents
    public DbSet<ContentCategoryEntity> ContentCategories { get; set; }
    public DbSet<ContentEntity> Contents { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }

    // Authentications
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            _builder?.OnConfiguring(optionsBuilder);

            base.OnConfiguring(optionsBuilder);
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

            modelBuilder.Entity<ContentCategoryEntity>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.Key).IsUnique();
                model.Property(x => x.Key).UseCollation("SQL_Latin1_General_CP1_CS_AS");
            });

            modelBuilder.Entity<ContentEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Category)
                .WithMany(x => x.Contents)
                .HasForeignKey(x => x.CategoryId);

                model.HasOne(x => x.Language)
                .WithMany(x => x.Contents)
                .HasForeignKey(x => x.LanguageId);
            });

            modelBuilder.Entity<LanguageEntity>(model =>
            {
                model.HasData(
                    new LanguageEntity()
                    {
                        Id = 1,
                        Name = "fa-IR",
                        CreationDateTime = DateTime.Now
                    },
                    new LanguageEntity()
                    {
                        Id = 2,
                        Name = "en-US",
                        CreationDateTime = DateTime.Now
                    }
                );

                model.HasKey(x => x.Id);
                model.HasIndex(x => x.Name).IsUnique();
            });
            
            modelBuilder.Entity<UserRoleEntity>(model =>
            {
                model.HasKey(ur => new { ur.UserId, ur.RoleId });
                
                model
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
                
                model
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);
            });
            
            base.OnModelCreating(modelBuilder);
    }
}