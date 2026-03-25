using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using static Abstracciones.Modelos.ProductoBase;

namespace Web.Pages.Productos
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuration;
        public ProductoResponse producto { get; set; } = default!;

        public DetalleModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "ObtenerProductoPorId");

            var cliente = new HttpClient();

            // 🔥 ESTA ES LA LÍNEA CLAVE
            cliente.BaseAddress = new Uri(_configuration.ObtenerValor("APIEnpoints", "UrlBase"));

            var url = string.Format(endpoint, id);

            var respuesta = await cliente.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

            return Page();
        }
    }
}
