using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using User.Domain.Models.Entities;

namespace User.Domain.Repositories;

public class DataContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<JustUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}