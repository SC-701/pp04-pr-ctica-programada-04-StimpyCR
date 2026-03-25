using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;
using static Abstracciones.Modelos.ProductoBase;

namespace Web.Pages.Productos
{
    [Authorize]
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

            using var cliente = ObtenerClienteConToken();  // ★

            cliente.BaseAddress = new Uri(urlBase);
            var respuesta = await cliente.GetAsync(endpoint);

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);
        }
        // ★ Helper — extrae el JWT de los claims y configura el HttpClient
        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "AccessToken");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
    }
}
