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

            var metodos = _configuration
                .GetSection($"{seccion}:Metodos")
                .Get<List<MetodoConfig>>();

            var metodo = metodos?.FirstOrDefault(m => m.Nombre == nombre);

            return metodo?.Valor ?? string.Empty;
        }

        public string ObtenerValor(string seccion, string llave)
        {
            if (string.IsNullOrWhiteSpace(seccion) || string.IsNullOrWhiteSpace(llave))
                return string.Empty;

            return _configuration[$"{seccion}:{llave}"] ?? string.Empty;
        }
        public class MetodoConfig
        {
            public string Nombre { get; set; }
            public string Valor { get; set; }
        }
    }
}
