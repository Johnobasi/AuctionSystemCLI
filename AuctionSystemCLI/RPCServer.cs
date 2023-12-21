using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystemCLI
{
    public class RPCServer
    {
        private const int port = 8888; // Change port number if needed
        private TcpListener listener;

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            // Start listening for connections in a separate thread
            Task.Run(() => ListenForClients());
        }

        public void Stop()
        {
            listener.Stop();
        }

        private async void ListenForClients()
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                // Process the client's request in a separate thread
                Task.Run(() => HandleClientRequest(client));
            }
        }

        private void HandleClientRequest(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string request = Encoding.ASCII.GetString(data, 0, bytesRead);

                // Process the request based on the RPC message format
                ProcessRequest(request);
            }

            client.Close();
        }

        private void ProcessRequest(string request)
        {
            
            string[] parts = request.Split('|');

            if (parts.Length > 0)
            {
                string messageType = parts[0];

                switch (messageType)
                {
                    case "NEW_AUCTION":
                        //Console.WriteLine("New auction is created successfully!");
                        break;

                    case "NEW_BID":
                        //Console.WriteLine("Bid was placed successfully!");
                        break;

                    case "AUCTION_OUTCOME":
                        //Console.WriteLine("Auction outcome");
                        break;

                    default:
                        //Console.WriteLine("Unknown RPC message type.");
                        break;
                }
            }
        }
    }
}
