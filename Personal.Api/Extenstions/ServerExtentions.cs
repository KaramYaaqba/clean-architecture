using System;
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace Personal.Api.Extenstions;

public static class ServerExtentions
{
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    public static void ConfigureLoggerService(this IServiceCollection services) => 
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    });

    public static WebApplication MapMiddleware(this WebApplication app){
        app.Use(async (context, next) =>
        {
            Console.WriteLine($"Logic before executing the next delegate in the Use method");
            await next.Invoke();
            Console.WriteLine($"Logic after executing the next delegate in the Use method");
        });


        app.MapWhen(context => context.Request.Query.ContainsKey("filter"), builder => {
            builder.Run(async context => {
                await context.Response.WriteAsync("hello from karam condition");
            });
        });
        app.Run(async context =>
        {
            Console.WriteLine($"Writing the response to the client in the Run method");
        });
        return app;
    }
}
