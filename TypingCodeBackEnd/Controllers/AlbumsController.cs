using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TypingCodeBackEnd.Models;

namespace TypingCodeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ILogger<AlbumsController>? _logger;
        private readonly string? _url = "https://jsonplaceholder.typicode.com/";
        private HttpClient? _httpClient;
        public AlbumsController(ILogger<AlbumsController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        [HttpGet]
        [Route("{id:int}/photos")]
        public async Task<ActionResult<List<Photo>>> GetPhotos(int id)
        {
            List<Photo> photos = new List<Photo>();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/albums/" + id + "/photos");
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation($"Obteniendo fotos del usuario id:  {id}");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                photos = JsonConvert.DeserializeObject<List<Photo>>(postsResponse)!;
            }
            else
            {
                _logger?.LogError($"Error al obtener las fotos del usuario id: {id}");
            }

            return Ok(photos);

        }


    }
}
