namespace ZAM.Pi.Infrastructure.MassTransit.Consumers;

using global::MassTransit;
using ZAM.Shared.Interfaces;

internal sealed class GotRoomTemperatureConsumer : IConsumer<GetRoomTemperature>
{
    public async Task Consume(ConsumeContext<GetRoomTemperature> context)
    {
        Console.WriteLine("Consuming GetRoomTemperature");
        Console.WriteLine($"Room number from request: {context.Message.RoomNumber}");

        await Task.CompletedTask;
    }
}
