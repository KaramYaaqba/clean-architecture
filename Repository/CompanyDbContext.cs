using System;
using System.Dynamic;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Configuration;

namespace Repository;

public class CompanyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Company> Companies { set; get; }
    public DbSet<Employee> Employees { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}
