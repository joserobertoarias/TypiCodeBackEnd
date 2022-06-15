using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TypingCodeBackEnd.Models;

namespace TypingCodeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController>? _logger;
        private readonly string? _url = "https://jsonplaceholder.typicode.com/";
        private HttpClient? _httpClient;
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        //api/users
        public async Task<ActionResult<List<User>>> Get()
        {
            List<User> users = new List<User>();
            HttpResponseMessage resp = await _httpClient!.GetAsync("/users");
            if (resp.IsSuccessStatusCode)
            {
                var userResponse = resp.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(userResponse)!;

            }

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}/posts")]
        //api/users/1/Posts
        public async Task<ActionResult<Post>> GetPosts(int id)
        {
            List<Post> posts = new List<Post>();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/users/" + id + "/posts");
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation($"Obteniendo posts del usuario id: {id}");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Post>>(postsResponse)!;
            }
            else
            {
                _logger?.LogError($"Error al obtener posts del usuario id: {id} ");

            }

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:int}/albums")]
        //api/users/1/Posts
        public async Task<ActionResult<Album>> GetAlbums(int id)
        {
            List<Album> albums = new List<Album>();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/users/" + id + "/posts");
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation($"Obteniendo posts del usuario id: {id}");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                albums = JsonConvert.DeserializeObject<List<Album>>(postsResponse)!;
            }
            else
            {
                _logger?.LogError($"Error al obtener posts del usuario id: {id} ");

            }

            return Ok(albums);
        }


    }
}
