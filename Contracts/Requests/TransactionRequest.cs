namespace Contracts.Requests;

public record TransactionRequest(string Source, string Destination, long Amount);
