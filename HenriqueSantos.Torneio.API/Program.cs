using HenriqueSantos.Torneio.API.Data;
using HenriqueSantos.Torneio.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TorneioContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapGet("/torneio", async (TorneioContext context) => await context.Campeonatos.ToListAsync())
    .WithName("GetTorneio")
    .WithTags("Torneio");

app.MapGet("/torneio/{id}", async (
    Guid id,
    TorneioContext context) =>
    await context.Campeonatos.FindAsync(id) is Campeonato campeonato ? Results.Ok(campeonato) : Results.NotFound())
    .Produces<Campeonato>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetTorneioPorId")
    .WithTags("Torneio");

app.MapPost("/torneio", async (TorneioContext context, Campeonato campeonato) =>
{
    if (!MiniValidation.MiniValidator.TryValidate(campeonato, out var errors))
        return Results.ValidationProblem(errors);

    await context.AddAsync(campeonato);
    var result = await context.SaveChangesAsync();

    return result > 0 ? Results.CreatedAtRoute("GetTorneioPorId", new { id = campeonato.Id }) : 
        Results.BadRequest("Não foi possível salvar o registro");

}).ProducesValidationProblem()
.Produces<Campeonato>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("PostTorneio")
.WithTags("Torneio");

app.Run();
