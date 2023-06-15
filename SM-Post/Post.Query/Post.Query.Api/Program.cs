using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastruct.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
void Options(DbContextOptionsBuilder options) => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<DatabaseContext>(Options);
builder.Services.AddSingleton(new DatabaseContextFactory(Options));

var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
