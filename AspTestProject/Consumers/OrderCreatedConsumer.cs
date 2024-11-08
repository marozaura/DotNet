using System.Text.Json;
using MassTransit;
using SharedModels;

namespace AspTestProject.Consumers
{
    public class OrderCreatedConsumer : IConsumer<IOrderCreated>
    {
        public async Task Consume(ConsumeContext<IOrderCreated> context)
        {
            await Task.CompletedTask;
        }
    }
}
