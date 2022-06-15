using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using TypingCodeBackEnd.Models;

namespace TypingCodeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogger<LogsController>? _logger;
        private readonly IWebHostEnvironment _env;

        public LogsController(ILogger<LogsController>? logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;

        }

        [HttpGet]
        public async Task<ActionResult<List<Log>>> Get()
        {
            List<Log> lstLogs = new List<Log>();


            string pathDir = Path.Combine(_env.ContentRootPath + "wwwroot\\Content\\logs\\");

            if (Directory.Exists(pathDir))
            {
                foreach (var item in Directory.GetFiles(pathDir))
                {
                    FileInfo info = new FileInfo(item);
                    lstLogs.Add(new Log { fileName = info.Name, dateCreated = info.CreationTime });
                }
            }

            return Ok(lstLogs);
        }


        [HttpPost]
        [Route("file")]
        public async Task<ActionResult<List<string>>> GetFile([FromBody] string fileName)
        {
            List<string> lst = new List<string>();

            string pathFile = Path.Combine(_env.ContentRootPath + "wwwroot\\Content\\logs\\" + fileName);

            if (System.IO.File.Exists(pathFile))
            {
                using (FileStream stream = System.IO.File.Open(pathFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            lst.Add(line);
                        }
                    }
                }

            }

            return lst;
        }

    }
}
