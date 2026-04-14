using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowMVC", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowMVC");
app.UseAuthorization();

app.MapGet("/api/Movies/top-rated", async ([FromServices] IHttpClientFactory httpFactory) =>
{
    try
    {
        var http = httpFactory.CreateClient();
        http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwYTA5ZjAyNGZiMzI2ZDZhMjc0MmRlNTc3ZWEzNDg1YiIsIm5iZiI6MTc3NTg0NDg0Ny4xNDEsInN1YiI6IjY5ZDkzZGVmMTQzN2I3YTE0NWQ5MjM3MyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.NbMw5AwON4iNSfpXjg8Rd3HzC0ZaapALOctm2QA0yLc");

        var response = await http.GetStringAsync("https://api.themoviedb.org/3/movie/now_playing?language=es-ES&page=1");

        using var doc = JsonDocument.Parse(response);
        var results = doc.RootElement.GetProperty("results");

        var movies = results.EnumerateArray().Select(m => new {
            id = m.GetProperty("id").GetInt32(),
            title = m.GetProperty("title").GetString(),
            posterUrl = $"https://image.tmdb.org/t/p/w500{m.GetProperty("poster_path").GetString()}"
        }).ToList();

        return Results.Ok(movies);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});

app.MapGet("/api/Movies/{id}", async ([FromServices] IHttpClientFactory httpFactory, int id) =>
{
    try
    {
        var http = httpFactory.CreateClient();
        http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwYTA5ZjAyNGZiMzI2ZDZhMjc0MmRlNTc3ZWEzNDg1YiIsIm5iZiI6MTc3NTg0NDg0Ny4xNDEsInN1YiI6IjY5ZDkzZGVmMTQzN2I3YTE0NWQ5MjM3MyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.NbMw5AwON4iNSfpXjg8Rd3HzC0ZaapALOctm2QA0yLc");

        var response = await http.GetAsync($"https://api.themoviedb.org/3/movie/{id}?language=es-ES");

        if (!response.IsSuccessStatusCode) return Results.NotFound();

        var content = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(content);
        var m = doc.RootElement;

        var movieData = new
        {
            id = m.GetProperty("id").GetInt32(),
            title = m.GetProperty("title").GetString(),
            overview = m.GetProperty("overview").GetString(),
            posterUrl = $"https://image.tmdb.org/t/p/w500{m.GetProperty("poster_path").GetString()}"
        };

        return Results.Ok(movieData);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});

app.Run();