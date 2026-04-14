using Microsoft.EntityFrameworkCore;
using Movies.API.BussinessLogic_Services_.Interfaces.NewFolder;
// Añade los using para Usuarios (ajusta según tus carpetas reales)
using Movies.API.BussinessLogic_Services_.Interfaces.User;
using Movies.API.BussinessLogic_Services_.Logic.Movies;
using Movies.API.BussinessLogic_Services_.Logic.User;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DataAccess_Repository_.Interfaces.User;
using Movies.API.DataAccess_Repository_.Logic;
using Movies.API.DataAccess_Repository_.Logic.Users;
using Movies.API.DatabasesConnections;

var builder = WebApplication.CreateBuilder(args);

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

// Inyección de Dependencias: Películas
builder.Services.AddScoped<IMoviesDA, MoviesDA>();
builder.Services.AddScoped<I_Movies_BL, Movies_BL>();

// Inyección de Dependencias: Usuarios (Esto quita el error de "Unable to resolve service")
builder.Services.AddScoped<IUserDA, UserDA>();
builder.Services.AddScoped<IUserBL, UserBL>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowRetroClub");
app.UseAuthorization();
app.MapControllers();

app.Run();