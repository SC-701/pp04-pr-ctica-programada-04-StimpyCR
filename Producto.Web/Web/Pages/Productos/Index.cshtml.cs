using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;
using static Abstracciones.Modelos.ProductoBase;
using Abstracciones.Interfaces.Reglas;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuration;
        public IList<ProductoResponse> productos { get; set; } = default!;

        public IndexModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {

            string urlBase = _configuration.ObtenerValor("APIEnpoints", "UrlBase");
            string endpoint = _configuration.ObtenerMetodo("APIEnpoints", "ObtenerProductos");
            Console.WriteLine($"{urlBase}{endpoint}");

            if (string.IsNullOrEmpty(urlBase))
                throw new Exception("UrlBase está vacía");

            if (string.IsNullOrEmpty(endpoint))
                throw new Exception("Endpoint está vacío");

            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(urlBase);
            var respuesta = await cliente.GetAsync(endpoint);

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);
        }
    }
}
