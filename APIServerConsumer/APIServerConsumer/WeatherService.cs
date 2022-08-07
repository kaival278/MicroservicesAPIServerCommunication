using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace APIServerConsumer
{
    public class WeatherService
    {
        private readonly IMongoCollection<WeatherModel> _weatherCollection;

        public WeatherService(
            IOptions<WeatherDBSettings> weatherStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                weatherStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                weatherStoreDatabaseSettings.Value.DatabaseName);

            _weatherCollection = mongoDatabase.GetCollection<WeatherModel>(
                weatherStoreDatabaseSettings.Value.WeatherForcastCollectName);
        }

        public async Task<List<WeatherModel>> GetAsync() =>
            await _weatherCollection.Find(_ => true).ToListAsync();

        public async Task<WeatherModel?> GetAsync(string id) =>
            await _weatherCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(WeatherModel newWeatherData) =>
            await _weatherCollection.InsertOneAsync(newWeatherData);

        public async Task UpdateAsync(string id, WeatherModel updatedWeather) =>
            await _weatherCollection.ReplaceOneAsync(x => x.Id == id, updatedWeather);

        public async Task RemoveAsync(string id) =>
            await _weatherCollection.DeleteOneAsync(x => x.Id == id);

    }
}

