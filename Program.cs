using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Definição das filas e outras configurações do RabbitMQ
            RabbitMQHelper.DeclareQueue(channel, "pedidos");
            RabbitMQHelper.DeclareQueue(channel, "pedidos_processados");

            // Consumidor 1: Recebe os pedidos da fila "pedidos", processa e registra no banco de dados
            var consumer1 = new EventingBasicConsumer(channel);
            consumer1.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine("Pedido recebido: {0}", mensagem);

                // Processamento do pedido e registro no banco de dados
                var pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);
                BancoDeDadosHelper.InserirPedido(pedido);

                // Envio do pedido processado para a fila "pedidos_processados"
                var pedidoProcessado = new PedidoProcessado
                {
                    Id = pedido.Id,
                    Status = "Processado",
                    DataProcessamento = DateTime.Now
                };
                var mensagemProcessada = JsonConvert.SerializeObject(pedidoProcessado);
                var bodyProcessado = Encoding.UTF8.GetBytes(mensagemProcessada);
                channel.BasicPublish(exchange: "", routingKey: "pedidos_processados", basicProperties: null, body: bodyProcessado);

                Console.WriteLine("Pedido processado e enviado para o sistema de envio/logística");
            };
            channel.BasicConsume(queue: "pedidos", autoAck: true, consumer: consumer1);

            // Consumidor 2: Recebe os pedidos processados da fila "pedidos_processados" e os envia para o sistema de envio/logística
            var consumer2 = new EventingBasicConsumer(channel);
            consumer2.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine("Pedido processado recebido: {0}", mensagem);

                // Envio do pedido processado para o sistema de envio/logística
                var pedidoProcessado = JsonConvert.DeserializeObject<PedidoProcessado>(mensagem);
                SistemaEnvioLogisticaHelper.EnviarPedidoParaLogistica(pedidoProcessado);
            };
            channel.BasicConsume(queue: "pedidos_processados", autoAck: true, consumer: consumer2);

            Console.WriteLine("Aguardando pedidos...");
            Console.ReadLine();
        }
    }
}
