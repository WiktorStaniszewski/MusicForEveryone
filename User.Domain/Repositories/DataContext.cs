using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using User.Domain.Models.Entities;

namespace User.Domain.Repositories;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<JustUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JustUser>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles")); // optional: name join table
    }
}