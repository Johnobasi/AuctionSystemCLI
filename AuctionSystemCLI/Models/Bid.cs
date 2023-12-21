namespace AuctionSystemCLI.Models
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

        public override string ToString()
        {
            return $"Auction ID: {AuctionId}\nBid Amount: {Amount}\nBidder: {Bidder}";
        }
    }
}
