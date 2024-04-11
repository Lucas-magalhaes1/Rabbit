using RabbitMQ.Client;
using System.Text;

class RabbitMQHelper
{
    private static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

    public static IConnection GetConnection()
    {
        return factory.CreateConnection();
    }

    public static IModel CreateChannel(IConnection connection)
    {
        return connection.CreateModel();
    }

    public static void DeclareQueue(IModel channel, string queueName)
    {
        channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public static void PublishMessage(IModel channel, string queueName, string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}
