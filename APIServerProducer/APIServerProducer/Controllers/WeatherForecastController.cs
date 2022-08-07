using Microsoft.AspNetCore.Mvc;
using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
namespace APIServerProducer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

   

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {

       
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostWeatherForecast")]
    public WeatherForecast Post(WeatherForecast weatherForecast)
    {
       

        //Rabbit MQ send Messages
        RabbitMQClient client = new RabbitMQClient(_configuration);
        
        client.SendMessage(weatherForecast);

        return weatherForecast;
    }

}

