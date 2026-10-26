using System;

namespace Acacia.Core.Exeptions;

public class CloudStorageException : Exception
{
    public CloudStorageException(string message) : base(message)
    {
    }

    public CloudStorageException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
