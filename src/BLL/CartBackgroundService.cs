
using CartService.BLL.Events;
using CartService.DAL;
using System.Text.Json;

namespace CartService.BLL;

public class CartBackgroundService : BackgroundService
{
    //ICartService _cartService;
    IServiceProvider _serviceProvider;

    public CartBackgroundService(IServiceProvider serviceProvider )
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        RabbitMqEventBus rabbitMqEventBus = new RabbitMqEventBus();

        while (!stoppingToken.IsCancellationRequested)
        {
            
            await rabbitMqEventBus.SubscribeAsync<string>("catalog", UpdateCart);

            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }

    private async Task UpdateCart(string data)
    {
        var productEvent = JsonSerializer.Deserialize<ProductPropertyUpdatedEvent>(data);
        using var scope=_serviceProvider.CreateAsyncScope();
        var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

        var product = cartService.GetItems().FirstOrDefault(p=> p.Id == productEvent.ProductId);
        product.Name = productEvent.Name;        
        cartService.UpdateItem(product);

        //_cartService.

    }

}
