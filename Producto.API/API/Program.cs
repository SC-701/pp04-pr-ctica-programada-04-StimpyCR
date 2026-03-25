using Abstracciones.Interfaces.AccesoADatos;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using AccesoADatos;
using AccesoADatos.Repositorios;
using Flujo;
using Reglas;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro para configuración y HttpClient del servicio de tipo de cambio
builder.Services.AddScoped<IConfiguracion, Configuracion>();
builder.Services.AddHttpClient<ITipoCambioDolar, TipoCambioServicio>();

// Reglas y Flujo
builder.Services.AddScoped<IProductoReglas, ProductoReglas>();
builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();

// Acceso a datos
builder.Services.AddScoped<IProductoAD, ProductoAD>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

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
