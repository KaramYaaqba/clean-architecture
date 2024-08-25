using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace Personal.Api.Common;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
{
    public CompanyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
        
        var builder = new DbContextOptionsBuilder<CompanyDbContext>()
        .UseSqlite(configuration.GetConnectionString("CompanyDatabase"), b => b.MigrationsAssembly("Personal.Api"));
        return new CompanyDbContext(builder.Options);
    }
}
