using Contracts.Enums;

namespace Contracts.Responses;

public class Response
{
    public TransactionStatus Status { get; }
    public string Comment { get; }

    public Response(TransactionStatus status, string comment)
    {
        Status = status;
        Comment = comment;
    }
}
