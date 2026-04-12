using Microsoft.EntityFrameworkCore;
using Movies.API.BussinessLogic_Services_.Interfaces.NewFolder;
using Movies.API.BussinessLogic_Services_.Logic.Movies;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DataAccess_Repository_.Logic;
using Movies.API.DatabasesConnections;

var builder = WebApplication.CreateBuilder(args);

// --- REGISTRO DE SERVICIOS (Dependency Injection) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configuración de Entity Framework y SQL Server
// Esto resuelve el error de 'DbContextOptions' y permite las migraciones
builder.Services.AddDbContext<ObjContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registro de la Capa de Datos (Repository)
builder.Services.AddScoped<IMoviesDA, MoviesDA>();

// 3. Registro de la Capa de Negocio (Services)
builder.Services.AddScoped<I_Movies_BL, Movies_BL>();

// ----------------------------------------------------

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();