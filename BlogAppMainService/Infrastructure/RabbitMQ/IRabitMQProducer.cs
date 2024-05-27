namespace Infrastructure.RabbitMQ
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(string queueName, string eventPattern, T message);
        public string RecivedMessage(string queueName);
    }
}
