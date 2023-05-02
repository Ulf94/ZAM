namespace ZAM.Core.Infrastructure.Common;

using System.Runtime.Serialization;

public abstract class InfrastructureException : Exception
{
    public abstract string Code { get; }
    public Guid Id { get; protected set; }

    protected InfrastructureException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    protected InfrastructureException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
