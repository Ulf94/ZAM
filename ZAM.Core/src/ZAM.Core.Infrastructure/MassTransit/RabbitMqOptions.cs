namespace ZAM.Core.Infrastructure.MassTransit;

internal sealed class RabbitMqOptions
{
    public static readonly string SectionName = "RabbitMq";
    public string HostName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ushort Port { get; set; } = 5672;
    public string User { get; set; } = string.Empty;
    public bool UseSsl { get; set; } = default;
    public string VirtualHost { get; set; } = string.Empty;
}
