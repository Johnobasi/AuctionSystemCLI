using AuctionSystemCLI.Models;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace AuctionSystemCLI
{
    public class AuctionManager
    {
        private List<Auction> auctions = new List<Auction>();

        public void StartAuction(string itemName, decimal startingPrice, string winner)
        {
            // Create a new auction
            Auction newAuction = new Auction(itemName, startingPrice, winner);
            auctions.Add(newAuction);

            // Inform other participants about the new auction
            RPCClient.BroadcastNewAuction(newAuction);
        }

        public void PlaceBid(int auctionId, decimal bidAmount, string bidder)
        {
            Auction auction = auctions.Find(a => a.Id == auctionId);

            if (auction != null)
            {
                bool bidAccepted = auction.AcceptBid(bidAmount, bidder);

                if (bidAccepted)
                {
                    // Notify other participants about the new bid
                    RPCClient.BroadcastBid(auction.Id, bidAmount, bidder);
                }
                else
                {
                    Console.WriteLine("Bid amount must be higher than the current highest bid.");
                }
            }
            else
            {
                Console.WriteLine("Auction not found.");
            }
        }

        public void FinalizeAuction(int auctionId, decimal finalAmount, string winner)
        {
            Auction auction = auctions.Find(a => a.Id == auctionId);

            if (auction != null)
            {
                bool success = auction.CloseAuction(finalAmount, winner);

                if (success)
                {
                    // Notify all participants about the auction outcome
                    RPCClient.BroadcastAuctionOutcome(auction.Id, finalAmount, winner);
                }
                else
                {
                    Console.WriteLine("Auction closing failed. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Auction not found.");
            }
        }

        public List<Auction> GetAuctions()
        {
            return auctions;
        }

    }
}
