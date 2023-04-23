namespace ZAM.Core.Application.Common.Exceptions;

using System.Runtime.Serialization;

public sealed class TokenNotFetchedException : ApplicationException
{
    public override string Code => "TOKEN_NOT_FETCHED";

    public TokenNotFetchedException()
        : base("Token was not fetched")
    {
    }

    public TokenNotFetchedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
