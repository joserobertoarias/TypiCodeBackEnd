using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TypingCodeBackEnd.Models;

namespace TypingCodeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly ILogger<PostsController>? _logger;
        private readonly string? _url = "https://jsonplaceholder.typicode.com/";
        private HttpClient? _httpClient;
        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        [HttpGet]
        //api/Posts
        public async Task<ActionResult<List<Post>>> Get()
        {
            List<Post> posts = new List<Post>();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/posts");
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation("Obteniendo todos los posts");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Post>>(postsResponse)!;
            }
            else
            {
                _logger?.LogError("Error al obtener todos los posts, ");
            }

            return Ok(posts);

        }

        [HttpGet]
        [Route("{id:int}/comments")]
        //api/Posts
        public async Task<ActionResult<Comment>> GetComments(int id)
        {
            List<Comment> comments = new List<Comment>();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/posts/" + id + "/comments");
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation($"Obteniendo comments del post id: {id}");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                comments = JsonConvert.DeserializeObject<List<Comment>>(postsResponse)!;
            }
            else
            {
                _logger?.LogError($"Error al obtener comments del post id: {id} ");

            }

            return Ok(comments);
        }


        [HttpGet("{id:int}")]
        //api/Posts
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post posts = new Post();

            HttpResponseMessage resp = await _httpClient!.GetAsync("/posts/" + id);
            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation($"Obteniendo posts del usuario id: {id}");

                var postsResponse = resp.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<Post>(postsResponse)!;
            }
            else
            {
                _logger?.LogError($"Error al obtener posts del usuario id: {id} ");

            }

            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Post post)
        {
            Post postResult = new Post() { Body = post.Body, Title = post.Title, UserId = post.UserId, Id = 0 };

            var jSon = JsonConvert.SerializeObject(post);
            var data = new StringContent(jSon, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _httpClient!.PostAsync("/posts", data);

            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation("Enviando un nuevo post");

                postResult = JsonConvert.DeserializeObject<Post>(resp.Content.ReadAsStringAsync().Result)!;
            }
            else
            {
                _logger?.LogError("Error. al enviar un nuevo post");

            }

            return Ok(postResult);
        }


        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Post post)
        {
            Post postResult = new Post() { Body = post.Body, Title = post.Title, UserId = post.UserId, Id = post.Id };

            var jSon = JsonConvert.SerializeObject(post);
            var data = new StringContent(jSon, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _httpClient!.PutAsync("/posts", data);

            if (resp.IsSuccessStatusCode)
            {
                _logger?.LogInformation("Actualizando  post");

                postResult = JsonConvert.DeserializeObject<Post>(resp.Content.ReadAsStringAsync().Result)!;
            }
            else
            {
                _logger?.LogError("Actualizando  post");

            }
            return Ok(postResult);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage resp = await _httpClient!.DeleteAsync("/posts" + id);

            _logger?.LogInformation("Eliminando  post");

            return Ok(resp);

        }






    }
}
