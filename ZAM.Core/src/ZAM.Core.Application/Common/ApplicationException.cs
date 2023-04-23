namespace ZAM.Core.Application.Common;

using System.Runtime.Serialization;

public abstract class ApplicationException : Exception
{
    public abstract string Code { get; }
    public Guid Id { get; protected set; }

    protected ApplicationException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    protected ApplicationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
