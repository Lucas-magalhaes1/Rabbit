using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Newtonsoft.Json;

class PedidoProcessadoConsumer
{
    public void StartConsuming()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "pedidos_processados",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine("Pedido processado recebido: {0}", mensagem);

                var pedidoProcessado = JsonConvert.DeserializeObject<PedidoProcessado>(mensagem);
                SistemaEnvioLogisticaHelper.EnviarPedidoParaLogistica(pedidoProcessado);

                Console.WriteLine("Pedido enviado para o sistema de envio/logística");
            };

            channel.BasicConsume(queue: "pedidos_processados",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Aguardando pedidos processados...");
            Console.ReadLine();
        }
    }
}

