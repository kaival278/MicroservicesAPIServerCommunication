using APIServerConsumer;
using MongoDB.Bson.Serialization.IdGenerators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<WeatherDBSettings>(
    builder.Configuration.GetSection("WeatherDatabase"));
builder.Services.AddSingleton<WeatherService>();

MongoDB.Bson.Serialization.BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

