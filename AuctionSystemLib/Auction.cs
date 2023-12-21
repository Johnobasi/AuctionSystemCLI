using System;
using System.Collections.Generic;

namespace AuctionSystemLib.Models
{
    public class Auction
    {
        public int Id { get; }
        public string ItemName { get; }
        public decimal StartingPrice { get; }
        public decimal HighestBid { get; private set; }
        public string Winner { get; private set; }


        private List<Bid> bids;

        public Auction(int id, string itemName, decimal startingPrice, decimal highestBid, string winner)
        {
            Id = id;
            ItemName = itemName;
            StartingPrice = startingPrice;
            bids = new List<Bid>();
            HighestBid = highestBid;
            Winner = winner;
        }

        public bool PlaceBid(decimal bidAmount, string winner)
        {
            if (bidAmount <= HighestBid)
                return false;

            HighestBid = bidAmount;
            Winner = winner;
            Bid newBid = new Bid(Id, bidAmount, winner);
            bids.Add(newBid);
            return true;

        }

        public List<Bid> GetBids()
        {
            return bids;
        }

        public bool CloseAuction(out decimal finalAmount, out string winner)
        {
            if (bids.Count == 0)
            {
                finalAmount = 0;
                winner = "No winner";
                return false;
            }

            finalAmount = HighestBid;
            winner = Winner;

            return true;
        }
    }
}
