using AuctionSystemCLI;

namespace AuctionSystemTest
{
    public class AuctionManagerTests
    {
        [Fact]
        public void StartAuction_AddsNewAuctionToList()
        {
            // Arrange
            AuctionManager auctionManager = new AuctionManager();
            int initialCount = auctionManager.GetAuctions().Count;

            // Act
            auctionManager.StartAuction("Test Item", 50.0m, "No Bids Yet");
            int finalCount = auctionManager.GetAuctions().Count;

            // Assert
            Assert.Equal(initialCount + 1, finalCount);
        }

        [Fact]
        public void PlaceBid_ValidBidAccepted()
        {
            // Arrange
            AuctionManager auctionManager = new AuctionManager();
            auctionManager.StartAuction("Test Item", 50.0m, "No Bids Yet");
            int auctionId = auctionManager.GetAuctions()[0].Id;
            decimal initialHighestBid = auctionManager.GetAuctions()[0].HighestBid;

            // Act
            auctionManager.PlaceBid(auctionId, 60.0m, "Test Bidder");

            // Assert
            decimal finalHighestBid = auctionManager.GetAuctions()[0].HighestBid;
            Assert.Equal(60.0m, finalHighestBid);
        }
    }
}
