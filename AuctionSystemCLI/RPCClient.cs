using AuctionSystemCLI.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AuctionSystemCLI
{
    public class RPCClient
    {
        private const int port = 139; // Change port number if needed
        private static readonly IPAddress serverIp = IPAddress.Parse("10.8.0.76"); // Replace with actual server IP from your computer : run (ipconfig/netstat -an) in cmd to find out

        public static void BroadcastNewAuction(Auction auction)
        {
            // Connect to the RPC server
            using (TcpClient client = new TcpClient(serverIp.ToString(), port))
            using (NetworkStream stream = client.GetStream())
            {
                string message = $"NEW_AUCTION|{auction.Id}|{auction.ItemName}|{auction.StartingPrice}|{auction.Winner}";
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                stream.Write(data, 0, data.Length);
            }
        }

        public static void BroadcastBid(int auctionId, decimal bidAmount, string bidder)
        {
            // Connect to the RPC server
            using (TcpClient client = new TcpClient(serverIp.ToString(), port))
            using (NetworkStream stream = client.GetStream())
            {
                string message = $"NEW_BID|{auctionId}|{bidAmount}|{bidder}";
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                stream.Write(data, 0, data.Length);
            }
        }

        public static void BroadcastAuctionOutcome(int auctionId, decimal finalAmount, string winner)
        {
            // Connect to the RPC server
            using (TcpClient client = new TcpClient(serverIp.ToString(), port))
            using (NetworkStream stream = client.GetStream())
            {
                string message = $"AUCTION_OUTCOME|{auctionId}|{finalAmount}|{winner}";
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
