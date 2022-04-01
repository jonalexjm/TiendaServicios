using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibroService> _logger;
        private readonly IConfiguration _configuration;
        public LibroService(IHttpClientFactory httpClient,
                            ILogger<LibroService> logger,
                            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<(bool resultado, LibroRemote libro, string errorMessage)> GetLibro(Guid libroId)
        {
            try
            {
                var urlLibros = _configuration.GetSection("Services:Libros").Value;
                var cliente = _httpClient.CreateClient();
                cliente.BaseAddress = new Uri($"{urlLibros}");
                var response = await cliente.GetAsync($"api/LibroMaterial/{libroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
