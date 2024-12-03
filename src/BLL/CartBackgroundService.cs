
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using CartService.BLL.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CartService.BLL;

public abstract class ConsumerBase<T> : BackgroundService
{
    //ICartService _cartService;
    //IServiceProvider _serviceProvider;

    protected abstract string ExchangeName { get; }
    protected abstract string ExchangeType { get; }
    protected abstract string RoutingKey { get; }
    protected abstract string QueueName { get; }
    protected IModel Channel { get; private set; }
    private IConnection _connection;
    private readonly IConnectionFactory _connectionFactory;

    public ConsumerBase(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        ConnectToRabbitMq();
    }

    private void ConnectToRabbitMq()
    {
        try
        {
            if (_connection == null || _connection.IsOpen == false)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (Channel == null || Channel.IsOpen == false)
            {
                Channel = _connection.CreateModel();

                Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType, durable: false, autoDelete: false);
                Channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false);
                Channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An exception occurred while configuring RabbitMQ.{Error}", ex.ToString());
        }
    }

    public virtual Task ProcessMessage(T message) => Task.CompletedTask;

    protected override  Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);

            consumer.Received += async (model, @event) =>
            {
                var message = Encoding.UTF8.GetString(@event.Body.ToArray());
                try
                {
                   

                   // _logger.LogInformation("FMT receive RabbitMq message with type ({typeMessage})", typeof(TMessage).GetGenericArguments()[0].FullName);
                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Include
                    };

                    var typedMessage = JsonConvert.DeserializeObject<T>(message, settings);
                    await ProcessMessage(typedMessage);

                }
                catch (Exception ex)
                {
                   // _logger.LogError(ex, "Error processing message :{Message}", ex.Message);
                }
                finally
                {
                    Channel.BasicAck(@event.DeliveryTag, false);
                }
            };

            Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

        }
        catch (Exception ex)
        {
            //_logger.LogCritical(ex, "Error while retrieving message from queue :{Message}", ex.Message);
        }

        return Task.CompletedTask;


    }

    }

    public class CartConsumer : ConsumerBase<ProductPropertyUpdatedEvent>
    {
        protected override string ExchangeName => "catalog";
        protected override string ExchangeType => "topic";
        protected override string RoutingKey => "catalog";
        protected override string QueueName => "catalog";

        

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CartConsumer(IServiceScopeFactory serviceScopeFactory, IConnectionFactory connectionFactory) : base(connectionFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task ProcessMessage(ProductPropertyUpdatedEvent productEvent)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();


            var product = cartService.GetItems().FirstOrDefault(p => p.Id == productEvent.ProductId);
            product.Name = productEvent.Name;
            cartService.UpdateItem(product);

            return base.ProcessMessage(productEvent);
        }



    }



