using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        public List<Pelicula> Peliculas { get; private set; } = new List<Pelicula>();
        private readonly ILogger<VentasModel> _logger;
        public string Message { get; private set; }
        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                // Intenta obtener la lista de películas
                Peliculas = await _apiService.GetAllPeliculasAsync();
            }
            catch (HttpRequestException ex)
            {
                // Manejo de excepciones específicas de HTTP
                Message = "No se pudieron obtener las películas. Por favor, inténtalo de nuevo más tarde.";
                _logger.LogError("Error al obtener películas: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones generales
                Message = "Ocurrió un error inesperado. Por favor, inténtalo de nuevo más tarde.";
                _logger.LogError("Error inesperado: {Message}", ex.Message);
            }
        }

    }
}
