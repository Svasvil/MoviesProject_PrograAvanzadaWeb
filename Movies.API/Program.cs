using Microsoft.EntityFrameworkCore;
using Movies.API.BussinessLogic_Services_.Interfaces.NewFolder;
using Movies.API.BussinessLogic_Services_.Logic.Movies;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DataAccess_Repository_.Logic;
using Movies.API.DatabasesConnections;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowRetroClub", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de Datos
builder.Services.AddDbContext<ObjContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de Dependencias
builder.Services.AddScoped<IMoviesDA, MoviesDA>();
builder.Services.AddScoped<I_Movies_BL, Movies_BL>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// EL ORDEN ES VITAL: Routing -> CORS -> Auth -> Controllers
app.UseRouting();

app.UseCors("AllowRetroClub");

app.UseAuthorization();

app.MapControllers();

app.Run();