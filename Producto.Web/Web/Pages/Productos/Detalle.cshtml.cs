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

        public async Task OnGet(Guid? id)
        {
            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "ObtenerProductoPorId");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
        }
    }
}
