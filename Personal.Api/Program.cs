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
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddControllers()
.AddApplicationPart(typeof(Personal.Presentation.AssemblyReference).Assembly);
builder.Services.AddSqlite<CompanyDbContext>(connString);
builder.Services.ConfigureCors();

var app = builder.Build();
//app.UseHsts();
//app.UseHttpsRedirection();
//app.UseRouting();
//app.MapMiddleware();
//app.MigrateDb();
//app.MapArticleEndpoints();
app.MapControllers();
app.Run();
