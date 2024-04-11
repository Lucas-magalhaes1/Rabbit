using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Newtonsoft.Json;

class PedidoConsumer
{
    public void StartConsuming()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "pedidos",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine("Pedido recebido: {0}", mensagem);

                
                var pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);


                Console.WriteLine("Pedido processado com sucesso.");
            };

            channel.BasicConsume(queue: "pedidos",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Aguardando pedidos...");
            Console.ReadLine();
        }
    }
}

