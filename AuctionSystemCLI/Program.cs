using AuctionSystemCLI.Models;
using System;

namespace AuctionSystemCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            RPCServer server = new RPCServer();
            RPCClient client = new RPCClient();
            AuctionManager auctionManager = new AuctionManager();

            // Start RPC server
            server.Start();

            try
            {

                while (true)
                {

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------------");
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("----Auction App----");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------------\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;

                    var allAuctions = auctionManager.GetAuctions();

                    if (allAuctions != null)
                    {
                        Console.WriteLine("Available Auctions:\n");
                        foreach (var auction in allAuctions)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($"Auction ID: {auction.Id}\t| Item: {auction.ItemName}\t| Current Highest Bid: {auction.HighestBid}\t| Winner : {auction.Winner}");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    if (allAuctions != null)
                    {

                        Console.WriteLine("All recent bids :\n");
                        foreach (var auction in allAuctions)
                        {
                            if (auction.GetBids() != null)
                            {
                                var allBids = auction.GetBids();

                                foreach (var bid in allBids)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine($"{bid.Bidder} has placed the current highest bid of {bid.Amount}USD for the auction \"{bid.AuctionId}\"");
                                }
                            }
                        }

                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\nChoose an option:\n");
                    Console.WriteLine("1. Start an auction");
                    Console.WriteLine("2. Place a bid");
                    Console.WriteLine("3. View auctions");
                    Console.WriteLine("4. Exit");

                    string choice = Console.ReadLine();
                    Console.Clear();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Enter auction details:\n");
                            Console.WriteLine("Item Name:");
                            string itemName = Console.ReadLine();
                            Console.WriteLine("Starting price:");
                            decimal startingPrice = Convert.ToDecimal(Console.ReadLine());
                            string winner = "No Bids Yet";
                            auctionManager.StartAuction(itemName, startingPrice, winner);
                            Console.Clear();
                            break;

                        case "2":
                            if (allAuctions != null)
                            {
                                Console.WriteLine("Here are the available auctions that you can bid:\n");
                                foreach (var auction in allAuctions)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine($"Auction ID: {auction.Id}\t| Item: {auction.ItemName}\t| Current Highest Bid: {auction.HighestBid}\t| Winner : {auction.Winner}");
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nEnter bid details:\n");
                            Console.WriteLine("Auction ID:");
                            int auctionId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Bid Amount:");
                            decimal bidAmount = Convert.ToDecimal(Console.ReadLine());
                            Console.WriteLine("Enter your name:");
                            string bidder = Console.ReadLine();
                            auctionManager.PlaceBid(auctionId, bidAmount, bidder);
                            Console.Clear();
                            break;

                        case "3":
                            // View auctions
                            foreach (var auction in auctionManager.GetAuctions())
                            {
                                Console.WriteLine($"Auction ID: {auction.Id}\t| Item: {auction.ItemName}\t| Current Highest Bid: {auction.HighestBid}\t| Winner : {auction.Winner}");
                            }
                            Console.WriteLine("\nPress Enter to continue...");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case "4":
                            server.Stop();
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while running the application : ", e.Message);
            }
        }
    }
}
