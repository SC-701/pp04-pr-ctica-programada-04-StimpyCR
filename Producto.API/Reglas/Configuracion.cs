using Abstracciones.Interfaces.Reglas;
using Microsoft.Extensions.Configuration;


namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            if (string.IsNullOrWhiteSpace(seccion) || string.IsNullOrWhiteSpace(nombre))
                return string.Empty;

            var section = _configuration.GetSection(seccion);
            var valor = section?[nombre] ?? section?.GetSection(nombre).Value;
            return valor ?? string.Empty;
        }

        public string ObtenerValor(string llave)
        {
            if (string.IsNullOrWhiteSpace(llave))
                return string.Empty;

            return _configuration[llave] ?? string.Empty;
        }
    }
}
