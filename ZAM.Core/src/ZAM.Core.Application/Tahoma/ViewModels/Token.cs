namespace ZAM.Core.Application.Tahoma.ViewModels;

using System.Text.Json.Serialization;

public class Token
{
    [JsonPropertyName("token")] public int Value { get; set; } = default;
}
