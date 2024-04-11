# Relatório do Experimento com RabbitMQ

## Introdução
Este relatório descreve o experimento realizado com RabbitMQ para implementar um sistema de mensageria em uma loja online. O experimento envolveu a criação de produtores e consumidores de mensagens para processar pedidos e enviar pedidos processados para o sistema de envio/logística.

## Linguagem de Programação Escolhida
Para este experimento, foi utilizada a linguagem de programação C# devido à familiaridade e à facilidade de integração com o RabbitMQ através da biblioteca RabbitMQ.Client.

## Código Desenvolvido
Foram desenvolvidas as seguintes classes para implementar o sistema de mensageria:
- `PedidoConsumer.cs`: Consumidor de mensagens responsável por processar os pedidos recebidos da loja online.
- `PedidoProcessadoConsumer.cs`: Consumidor de mensagens responsável por enviar os pedidos processados para o sistema de envio/logística.
- `BancoDeDadosHelper.cs`: Classe auxiliar para interagir com o banco de dados da loja, inserindo os pedidos processados.
- `SistemaEnvioLogisticaHelper.cs`: Classe auxiliar para enviar os pedidos processados para o sistema de envio/logística.
- `Pedido.cs`: Classe que representa um pedido na loja online.
- `PedidoProcessado.cs`: Classe que representa um pedido processado.

O código foi organizado em diferentes arquivos e pastas para facilitar a manutenção e a compreensão.

## Mensagens Trocadas
Durante o experimento, foram trocadas mensagens no formato JSON contendo informações dos pedidos. As mensagens foram enviadas para as filas do RabbitMQ e consumidas pelos consumidores correspondentes.

## Análise dos Resultados
O experimento foi bem-sucedido na implementação do sistema de mensageria utilizando RabbitMQ. Os pedidos foram processados corretamente e enviados para o sistema de envio/logística. A integração com o banco de dados também ocorreu sem problemas.

## Dificuldades Encontradas
Uma das dificuldades encontradas foi a configuração inicial do RabbitMQ, que exigiu conhecimento prévio sobre o serviço. Além disso, foi necessário lidar com a serialização e desserialização de objetos para troca de mensagens, o que exigiu atenção para evitar erros.

## Conclusão
O experimento demonstrou a viabilidade e a eficácia do RabbitMQ na implementação de sistemas de mensageria em aplicações distribuídas. A linguagem de programação C# foi adequada para este projeto devido à sua compatibilidade com o RabbitMQ e à facilidade de integração com outros sistemas.
