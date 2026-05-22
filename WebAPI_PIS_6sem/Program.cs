using Microsoft.EntityFrameworkCore;
using WebAPI_PIS_6sem.Data;
using WebAPI_PIS_6sem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

// ВАЖНО: Создание БД ДО любого middleware и маппинга
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RuleDbContext>();
    db.Database.EnsureCreated();
}

app.UseCors();
app.MapControllers();

app.Run();