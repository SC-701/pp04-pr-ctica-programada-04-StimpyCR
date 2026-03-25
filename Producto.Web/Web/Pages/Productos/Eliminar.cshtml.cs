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


        public async Task<ActionResult> OnGet(Guid? id)
            
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "ObtenerProductoPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
            return Page();
        }

        public async Task<ActionResult> OnPost(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "EliminarProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}
