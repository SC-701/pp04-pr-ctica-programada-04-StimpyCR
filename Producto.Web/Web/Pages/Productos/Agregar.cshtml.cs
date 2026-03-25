using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Abstracciones.Modelos.ProductoBase;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuration;

        public AgregarModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }
        [BindProperty]
        public ProductoRequest producto { get; set; }

        public void OnGet()
        {
        }
    }
}
