using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIServerConsumer;

public class WeatherModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("WeatherData")]
    public string date { get; set; } = null!;

    public float temperatureC { get; set; } 

    public float temperatureF { get; set; } 

    public string summary { get; set; } = null!;
}