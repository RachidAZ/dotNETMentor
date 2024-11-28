using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CartService.DAL;

public class RabbitMqEventBus : IMessageBus
{
    public async Task PublishAsync(string topicOrQueueName, object message)
    {

        throw new NotImplementedException();

        

    }

    public async Task SubscribeAsync<T>(string topicOrQueueName, Func<string, Task> onMessageReceived)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: topicOrQueueName, durable: false, exclusive: false, autoDelete: false,
            arguments: null);



        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
             onMessageReceived(message);

            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(topicOrQueueName, autoAck: true, consumer: consumer);


    }



}
