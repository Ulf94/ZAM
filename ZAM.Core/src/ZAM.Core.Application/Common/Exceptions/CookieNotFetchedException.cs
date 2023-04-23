namespace ZAM.Core.Application.Common.Exceptions;

using System.Runtime.Serialization;

[Serializable]
public sealed class CookieNotFetchedException : ApplicationException
{
    public override string Code => "COOKIE_NOT_RECEIVED";

    public CookieNotFetchedException()
        : base("Cookie was not received from external API")
    {
    }

    public CookieNotFetchedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
