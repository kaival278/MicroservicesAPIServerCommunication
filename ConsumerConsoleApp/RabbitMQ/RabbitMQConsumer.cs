using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace PaymentCardConsumer.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        
        private const string ExchangeName = "Topic_Exchange";
      


    
        private const string QueueName1 = "Topic_Queue1";
        private const string RoutingKey1 = "routingkeys.rountingkey1";
        public static string RestAPIUrl = "https://localhost:7171/api/WeatherForecast";


        public void CreateConnection()
        {
            _factory = new ConnectionFactory {
                HostName = "localhost",
                UserName = "guest", Password = "guest" };
          
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ProcessMessages()
        {
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    Console.WriteLine("Listening for Topic from RabbitMQ");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine();

                    channel.ExchangeDeclare(ExchangeName, "topic");
                    channel.QueueDeclare(QueueName1, 
                        true, false, false, null);

                    channel.QueueBind(QueueName1, ExchangeName, 
                       RoutingKey1);

                    channel.BasicQos(0, 10, false);
                    Subscription subscription = new Subscription(channel, 
                        QueueName1, false);
                    
                    while (true)
                    {
                        BasicDeliverEventArgs deliveryArguments = subscription.Next();

                        var message = 
                            (WeatherForecast)deliveryArguments.Body.DeSerialize(typeof(WeatherForecast));

                        var routingKey = deliveryArguments.RoutingKey;

                        Console.WriteLine("--- Payment - Routing Key <{0}> : {1} : {2}", routingKey, message, message);

                        WeatherPostModel weatherPostModel = new WeatherPostModel();
                        weatherPostModel.summary = message.Summary;
                        weatherPostModel.temperatureC = message.TemperatureC;
                        weatherPostModel.temperatureF =message.TemperatureF;
                        weatherPostModel.date = message.Date.ToShortDateString();

                       
                        RestAPICall restAPICall = new RestAPICall(RestAPIUrl);
                        restAPICall.postData(weatherPostModel);
                        subscription.Ack(deliveryArguments);
                    }
                }
            }
        }
    }
}
