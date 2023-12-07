using ApiService.Service;
using Domain.Interfaces;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Inyeccion de la capa de Infrastructura
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IProductService,ProductService>();

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
