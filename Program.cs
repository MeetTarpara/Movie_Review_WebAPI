using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Data;
using Movie_Review_WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<MovieCategoryRepository>();
builder.Services.AddScoped<StarRepository>();
builder.Services.AddScoped<MovieStarRepository>();






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
