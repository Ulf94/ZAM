namespace ZAM.Core.Infrastructure.Tahoma;

internal sealed class TahomaOptions
{
    public static readonly string SectionName = "Tahoma";
    public Uri BaseAddress { get; set; } = new Uri("about:blank");
    public int RetryCount { get; set; } = 3;
    public int RetrySleepDurationInMiliseconds { get; set; } = 5000;
    public int TimeoutInMiliseconds { get; set; } = 30000;
    public string UserId { get; init; } = string.Empty;
    public string UserPassword { get; init; } = string.Empty;
}
