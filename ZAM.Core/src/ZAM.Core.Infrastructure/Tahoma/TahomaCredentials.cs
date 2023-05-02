namespace ZAM.Core.Infrastructure.Tahoma;

public sealed class TahomaCredentials
{
    public static readonly string SectionName = "TahomaCredentials";
    public string UserId { get; init; } = string.Empty;
    public string UserPassword { get; init; } = string.Empty;
}
