using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Cinema.Pages
{
    public class VentasModel : PageModel
    {
        private readonly ApiService _apiService;
        private readonly ILogger<VentasModel> _logger;

        [BindProperty]
        public Venta Venta { get; set; } = new Venta();

        public Pelicula Pelicula { get; set; } = new Pelicula();
        public string Message { get; private set; }

        public VentasModel(ApiService apiService, ILogger<VentasModel> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int idPelicula)
        {
            // Obtiene los detalles de la película, incluyendo el precio del boleto
            Pelicula = await _apiService.GetMovieByIdAsync(idPelicula);

            if (Pelicula == null)
            {
                Message = "Película no encontrada.";
                return RedirectToPage("/Error");
            }

            return Page();
        }

       public async Task<IActionResult> OnPostAsync(int idPelicula, string nombreCliente, string metodoPago, int cantidadBoletos, int precioTotal, string pelicula)
        {
            // Guardar los datos del formulario en TempData para preservarlos al recargar y visualizar el banner de exito o error
            TempData["NombreCliente"] = nombreCliente;
            TempData["MetodoPago"] = metodoPago;
            TempData["CantidadBoletos"] = cantidadBoletos;
            TempData["PrecioTotal"] = precioTotal;
            TempData["Pelicula"] = pelicula;

            bool success = await _apiService.VentasPeliAsync(Venta);

            // Lógica de TempData para mensaje de resultado
            if (success)
            {
                TempData["MensajeVenta"] = "¡Se ha realizado la compra con éxito!";
                TempData["TipoMensaje"] = "success";
            }
            else
            {
                TempData["MensajeVenta"] = "Hubo un problema al realizar la compra. Inténtalo de nuevo.";
                TempData["TipoMensaje"] = "error";
            }

            // Siempre regresar a la misma página, manteniendo los datos en TempData
            return RedirectToPage();
        }
   }
}
