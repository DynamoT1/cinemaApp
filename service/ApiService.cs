using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Cinema.Models;
using Microsoft.Extensions.Configuration;

namespace Cinema.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pelicula>> GetAllPeliculasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Pelicula>>("peliculas/all-peliculas");
        }

        public async Task<Pelicula> GetMovieByIdAsync(int idPelicula)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Pelicula>($"peliculas/pelicula?idPelicula={idPelicula}");
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores en caso de que no se pueda conectar al servidor
                Console.WriteLine($"Error al obtener la película: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> VentasPeliAsync(Venta venta)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ventas/add-venta", venta);
                return response.IsSuccessStatusCode;
            }
            catch(HttpRequestException ex)
            {
                // Manejo de errores en caso de que no se pueda conectar al servidor
                Console.WriteLine($"Error al realizar la venta: {ex.Message}");
                return false;
            }
        }
    }
}
