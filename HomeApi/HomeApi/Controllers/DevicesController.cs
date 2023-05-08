using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHostEnvironment _env;

        // Инициализация конфигурации при вызове конструктора
        public DevicesController(ILogger<WeatherForecastController> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _env = hostEnvironment;
        }

        [HttpGet]
        [HttpHead]
        [Route("manufacturer")]
        public IActionResult GetManual([FromQuery] string manufacturer)
        {
            string manufacturerName = manufacturer.Split("/").Last();

            var staticPath = Path.Combine(_env.ContentRootPath, "Static");
            var filePath = Directory.GetFiles(staticPath)
                .FirstOrDefault(path => path.Split("\\")
                .Last()
                .Split(".")[0] == manufacturerName);

            if (filePath == null)
                return StatusCode(404, $"Инструкция {manufacturerName} не найдена на сервере.");

            string fileType = "application";
            string fileName = $"{manufacturerName}.pdf";

            return PhysicalFile(filePath, fileType, fileName);
        }
    }
}
