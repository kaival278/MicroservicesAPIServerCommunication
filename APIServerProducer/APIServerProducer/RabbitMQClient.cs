using System;
using System;
using System.Text;
using RabbitMQ.Client;
namespace APIServerProducer
{
    public class RabbitMQClient
    {

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        
        private const string ExchangeName = "Topic_Exchange";
        private const string QueueName1 = "Topic_Queue1";
        private const string QueueName2 = "Topic_Queue2";
        private const string AllQueueName = "AllTopic_Queue";

        private const string RoutingKey1= "routingkeys.rountingkey1";
        private const string RoutingKey2 = "routtingkeys.rountingkey1";
        private const string RoutingKeys = "routtingkeys.*";
        public RabbitMQClient(IConfiguration configuration)
        {
            CreateConnection(configuration);
        }

        private static void CreateConnection(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(ExchangeName, "topic");

            _model.QueueDeclare(QueueName1, true, false, false, null);
            _model.QueueDeclare(QueueName2, true, false, false, null);
            _model.QueueDeclare(AllQueueName, true, false, false, null);

            _model.QueueBind(QueueName1, ExchangeName, RoutingKey1);
            _model.QueueBind(QueueName2, ExchangeName,
               RoutingKey2);

           _model.QueueBind(AllQueueName, ExchangeName, RoutingKeys);
        }

        public void Close()
        {
            _connection.Close();
        }

       

        public void SendMessage(WeatherForecast weatherForecast)
        {
            PublishMessage(weatherForecast.Serialize(), RoutingKey1);
        }

        public void PublishMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }


    }
}


