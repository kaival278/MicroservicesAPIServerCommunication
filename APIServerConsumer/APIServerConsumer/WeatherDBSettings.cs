using System;
namespace APIServerConsumer
{
    public class WeatherDBSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string WeatherForcastCollectName { get; set; } = null!;
    }
}

