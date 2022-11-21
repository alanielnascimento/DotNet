using Microsoft.EntityFrameworkCore;
using PII.DATA;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = "Server=localhost;Database=BdDespesa;User=root;Password=root";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

builder.Services.AddDbContextPool<DataContext>(
    options => options.UseMySql(connectionString, serverVersion)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
