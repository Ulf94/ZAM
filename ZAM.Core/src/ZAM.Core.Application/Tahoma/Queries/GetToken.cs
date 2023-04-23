namespace ZAM.Core.Application.Tahoma.Queries;

using ZAM.Core.Application.Tahoma.Models;

public sealed class GetToken : IRequest<Token>
{
    public string Cookie { get; init; } = string.Empty;
}
