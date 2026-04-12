using Microsoft.EntityFrameworkCore;
using Movies.API.BussinessLogic_Services_.Interfaces.NewFolder;
using Movies.API.BussinessLogic_Services_.Logic.Movies;
using Movies.API.DataAccess_Repository_.Interfaces.Movies;
using Movies.API.DataAccess_Repository_.Logic;
using Movies.API.DatabasesConnections;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ObjContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMoviesDA, MoviesDA>();

builder.Services.AddScoped<I_Movies_BL, Movies_BL>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();