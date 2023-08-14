using BemolChallenge.Data;
using BemolChallenge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurando injeção de dependencia do Cosmos
builder.Services.AddDbContext<BemolContex>(option => option.UseCosmos("accountEndpoint", "accountKey", "databaseName"));

//Configurando injeção de dependencia do Service Bus
builder.Services.AddScoped<IAzureBusService, AzureBusService>();

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
