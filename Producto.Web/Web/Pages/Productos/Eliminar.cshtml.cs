using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;
using static Abstracciones.Modelos.ProductoBase;
using Abstracciones.Interfaces.Reglas;

namespace Web.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuration;
        public ProductoResponse producto { get; set; } = default!;

        public EliminarModel(IConfiguracion configuration)
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

            // 🔥 SOLUCIÓN SIMPLE
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

            if (producto == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "EliminarProducto");

            var cliente = new HttpClient();

            // 🔥 MISMA SOLUCIÓN AQUÍ
            cliente.BaseAddress = new Uri(_configuration.ObtenerValor("APIEnpoints", "UrlBase"));

            var url = string.Format(endpoint, id);

            var respuesta = await cliente.DeleteAsync(url);

            if (!respuesta.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
