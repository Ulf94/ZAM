namespace ZAM.Core.Application.Common.Exceptions;

using System.Runtime.Serialization;

public sealed class CookieNotFoundException : ApplicationException
{
    public override string Code => "COOKIE_NOT_FOUND";

    public CookieNotFoundException(string requestUrl)
        : base($"Cookie not found while sending request to url {requestUrl}")
    {
    }

    public CookieNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
