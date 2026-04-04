using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Flujo;
using DA;
using DA.Repositorios;
using Abstracciones.Interfaces.Reglas;
using Reglas;
using Abstracciones.Interfaces.Servicios;
using Servicios;
using Abstracciones.Modelos;             
using Microsoft.AspNetCore.Authentication.JwtBearer;  // ★
using Microsoft.IdentityModel.Tokens;                 // ★
using System.Text;                                    // ★
using Autorizacion.Middleware;                        // ★

var builder = WebApplication.CreateBuilder(args);

// ★ Leer configuración JWT y registrar autenticación
var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(tokenConfig.key))
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IVehiculoFlujo, VehiculoFlujo>();
builder.Services.AddScoped<IMarcaFlujo, MarcaFlujo>();
builder.Services.AddScoped<IModeloFlujo, ModeloFlujo>();
builder.Services.AddScoped<IVehiculoDA, VehiculoDA>();
builder.Services.AddScoped<IMarcaDA, MarcaDA>();
builder.Services.AddScoped<IModeloDA, ModeloDA>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IRegistroServicio, RegistroServicio>();
builder.Services.AddScoped<IRevisionServicio, RevisionServicio>();
builder.Services.AddScoped<IRegistroReglas, RegistroReglas>();
builder.Services.AddScoped<IRevisionReglas, RevisionReglas>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();

var politicaAcceso = "Politica de acceso";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: politicaAcceso,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost", "https://localhost:50427", "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(politicaAcceso);

app.UseAuthorization();

app.MapControllers();

app.Run();
