using Microsoft.AspNetCore.Mvc;
//https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
namespace APIServerConsumer.Controllers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherService _weatherService;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(WeatherService weatherService, ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<List<WeatherModel>> Get()
    {
         return await _weatherService.GetAsync();
  
    }
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<WeatherModel>> Get(string id)
    {
        var weatherModel = await _weatherService.GetAsync(id);

        if (weatherModel is null)
        {
            return NotFound();
        }

        return weatherModel;
    }

    [HttpPost]
    public async Task<IActionResult> Post(WeatherModel weatherModel)
    {
      
        await _weatherService.CreateAsync(weatherModel);
        return CreatedAtAction(nameof(Get), new { id = weatherModel.Id }, weatherModel);
    }
}

