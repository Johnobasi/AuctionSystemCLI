namespace AuctionSystemLib
{
    public class Bid
    {
        public int AuctionId { get; private set; }
        public decimal Amount { get; private set; }
        public string Bidder { get; private set; }

        public Bid(int auctionId, decimal amount, string bidder)
        {
            AuctionId = auctionId;
            Amount = amount;
            Bidder = bidder;
        }
    }
}
