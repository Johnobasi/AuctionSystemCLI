using System;
using System.Collections.Generic;

namespace AuctionSystemCLI.Models
{
    public class Auction
    {
        public int Id { get; private set; }
        public string ItemName { get; private set; }
        public decimal StartingPrice { get; private set; }
        public decimal HighestBid { get; private set; }
        public string Winner { get; private set; }

        private List<Bid> bids;

        private int GenerateRandomId()
        {
            Random rand = new Random();
            return rand.Next(1000, 10000); 
        }

        public List<Bid> GetBids()
        {
            return bids;
        }

        public Auction(string itemName, decimal startingPrice, string winner)
        {
            Id = GenerateRandomId();
            ItemName = itemName;
            StartingPrice = startingPrice;
            bids = new List<Bid>();
            HighestBid = startingPrice;
            Winner = winner;
        }

        public bool AcceptBid(decimal bidAmount, string bidder)
        {
            if (bidAmount > HighestBid)
            {
                Bid newBid = new Bid(Id, bidAmount, bidder);
                bids.Add(newBid);
                HighestBid = bidAmount;
                Winner = bidder;
                return true;
            }
            return false;
        }

        public bool CloseAuction(decimal finalAmount, string winner)
        {
            if (finalAmount >= HighestBid)
            {
                Winner = winner;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Auction ID: {Id}\nItem: {ItemName}\nStarting Price: {StartingPrice}\nHighest Bid: {HighestBid}\nWinner: {Winner}";
        }
    }
}
