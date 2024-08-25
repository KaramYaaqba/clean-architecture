using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Personal.Api.Entities;

namespace Personal.Api.Data;

public class BlogDBContext(DbContextOptions<BlogDBContext> options) : 
        DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Article> Articles => Set<Article>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>().HasData(
            new {Id = 1, Name = "Karam"},
            new {Id = 2, Name = "Yaaqba"}
        );
    }
}
