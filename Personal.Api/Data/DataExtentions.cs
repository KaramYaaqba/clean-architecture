using System;
using Microsoft.EntityFrameworkCore;

namespace Personal.Api.Data;

public static class DataExtentions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<BlogDBContext>();
        dbContext!.Database.Migrate();
    }
}
