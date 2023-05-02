namespace ZAM.Core.Infrastructure.Common.Exceptions;

using System.Runtime.Serialization;

public sealed class CredentialsNotFoundExceptions : InfrastructureException
{
    public override string Code => "CREDENTIALS_NOT_FOUND";

    public CredentialsNotFoundExceptions()
        : base("User ID or password not found")
    {
    }

    public CredentialsNotFoundExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
