using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendMessage<T>(string queueName,string eventPattern,T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, exclusive: false,autoDelete:false);
            var data = new
            {
                pattern = eventPattern,
                data = message
            };
            var json = JsonConvert.SerializeObject(data);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }

        public string RecivedMessage(string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);
            var consumer = new EventingBasicConsumer(channel);
            var result = channel.BasicGet(queueName, true);
            if (result != null)
            {
                var body = result.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                return message;
            }
            throw new Exception("No Message Found");
        }
    }
}
