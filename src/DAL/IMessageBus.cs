using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL;

public interface IMessageBus
{

    Task PublishAsync(string topicOrQueueName, object message);
    Task SubscribeAsync<T>(string topicOrQueueName, Func<string, Task> onMessageReceived);
}
