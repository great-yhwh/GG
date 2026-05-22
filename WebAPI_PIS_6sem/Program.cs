using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI_PIS_6sem.Data;
using WebAPI_PIS_6sem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PIS 6sem API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<RuleDbContext>(options =>
    options.UseSqlite("Data Source=rules.db"));

builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<RuleDirector>();
builder.Services.AddScoped<ServiceRule>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RuleDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIS 6sem API v1");
});

app.UseCors();
app.MapControllers();

app.Run();