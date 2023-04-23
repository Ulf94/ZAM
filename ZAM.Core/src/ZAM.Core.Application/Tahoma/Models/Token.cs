namespace ZAM.Core.Application.Tahoma.Models;

using System.Text.Json.Serialization;

public class Token
{
    [JsonPropertyName("token")]
    public string Value { get; set; } = string.Empty;
}
