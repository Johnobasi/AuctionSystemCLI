using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using AuctionSystemLib;
using AuctionSystemLib.Models;

namespace AuctionSystemLib
{
    public class DataAccess
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            connection = new SQLiteConnection("Data Source=auction.db;Version=3;");
            connection.Open();

            string createAuctionTableQuery = "CREATE TABLE IF NOT EXISTS Auctions (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "ItemName TEXT, " +
                "StartingPrice DECIMAL, " +
                "HighestBid DECIMAL, " +
                "Winner TEXT)";

            string createBidTableQuery = "CREATE TABLE IF NOT EXISTS Bids (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "AuctionId INTEGER, " +
                "Amount DECIMAL, " +
                "Bidder TEXT)";

            ExecuteNonQuery(createAuctionTableQuery);
            ExecuteNonQuery(createBidTableQuery);
        }

        private void ExecuteNonQuery(string query)
        {
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // CRUD operations for Auctions table
        public void AddAuction(Auction auction)
        {
            string query = "INSERT INTO Auctions (ItemName, StartingPrice, HighestBid, Winner) " +
                           $"VALUES ('{auction.ItemName}', {auction.StartingPrice}, {auction.HighestBid}, '{auction.Winner}')";

            ExecuteNonQuery(query);
        }

        public Auction GetAuctionById(int auctionId)
        {
            string query = $"SELECT * FROM Auctions WHERE Id = {auctionId}";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Populate auction object from database
                        return new Auction(
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToString(reader["ItemName"]),
                            Convert.ToDecimal(reader["StartingPrice"]),
                            Convert.ToDecimal(reader["HighestBid"]),
                            Convert.ToString(reader["Winner"])
                        );
                    }
                }
            }

            return null;
        }

        // CRUD operations for Bids table
        public void AddBid(Bid bid)
        {
            string query = "INSERT INTO Bids (AuctionId, Amount, Bidder) " +
                           $"VALUES ({bid.AuctionId}, {bid.Amount}, '{bid.Bidder}')";

            ExecuteNonQuery(query);
        }

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            List<Bid> bids = new List<Bid>();

            string query = $"SELECT * FROM Bids WHERE AuctionId = {auctionId}";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Populate bid objects from database
                        Bid bid = new Bid(
                            Convert.ToInt32(reader["AuctionId"]),
                            Convert.ToDecimal(reader["Amount"]),
                            Convert.ToString(reader["Bidder"])
                        );

                        bids.Add(bid);
                    }
                }
            }

            return bids;
        }

        public void UpdateAuction(Auction auction)
        {
            string query = $"UPDATE Auctions SET ItemName = '{auction.ItemName}', StartingPrice = {auction.StartingPrice}, " +
                           $"HighestBid = {auction.HighestBid}, Winner = '{auction.Winner}' WHERE Id = {auction.Id}";

            ExecuteNonQuery(query);
        }

        public void DeleteAuction(int auctionId)
        {
            string query = $"DELETE FROM Auctions WHERE Id = {auctionId}";

            ExecuteNonQuery(query);
        }

        public void UpdateBid(Bid bid)
        {
            string query = $"UPDATE Bids SET Amount = {bid.Amount}, Bidder = '{bid.Bidder}' WHERE AuctionId = {bid.AuctionId}";

            ExecuteNonQuery(query);
        }

        public void DeleteBid(int bidId)
        {
            string query = $"DELETE FROM Bids WHERE Id = {bidId}";

            ExecuteNonQuery(query);
        }

    }
}
