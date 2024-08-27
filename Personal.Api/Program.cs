using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLog;
using Personal.Api.Data;
using Personal.Api.Endpoints;
using Personal.Api.Extenstions;
using Repository;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));
builder.Services.ConfigureLoggerService();
var connString = builder.Configuration.GetConnectionString("companyDatabase");
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers()
.AddApplicationPart(typeof(Personal.Presentation.AssemblyReference).Assembly);
builder.Services.AddSqlite<CompanyDbContext>(connString);
builder.Services.ConfigureCors();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();