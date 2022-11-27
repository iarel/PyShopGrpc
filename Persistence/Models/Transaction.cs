namespace Persistence.Models;

public class Transaction : Entity
{
    public Transaction(int sourceId, int destionationId, int coinId) : base()
    {
        SourceId = sourceId;
        DestinationId = destionationId;
        CoinId = coinId;
    }

    public int SourceId { get; set; }
    public int DestinationId { get; set; }
    public int CoinId { get; set; }
}