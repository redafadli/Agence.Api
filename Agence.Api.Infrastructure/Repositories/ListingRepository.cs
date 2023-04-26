using System;
using System.Reflection.PortableExecutable;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

namespace Agence.Api.Infrastructure.Repositories {
    public class ListingRepository : IListingRepository {

        private SqlConnection _connection;
        private static IEnumerable<Listing>? Listings;

        public ListingRepository()
        {
            string connectionString = GetConnectionString();
            _connection = new SqlConnection(connectionString);
        }

        static private string GetConnectionString()
        {
            return "Data Source = localhost,1433; Database = MiCasa; Integrated Security = false; User ID = sa; Password = @Reda.2001";
        }

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term)
        {
            return await Task.Run(() => Listings.Where(l => l.Title.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {

            SqlCommand cmdGetListings = new SqlCommand("SELECT * FROM Listings", _connection);
            _connection.Open();
            SqlDataReader reader = cmdGetListings.ExecuteReader();
            List<Listing> Listingslist = new List<Listing>();
            while (reader.Read())
            {
                {
                    Listing newListing = new Listing();
                    newListing.Id = reader.GetInt32(0);
                    newListing.Title = reader.GetString(1);
                    newListing.Price = reader.GetDouble(2);
                    newListing.City = reader.GetString(3);
                    newListing.Description = reader.GetString(4);
                    newListing.Address = reader.GetString(5);
                    Listingslist.Add(newListing);
                };

            }
            _connection.Close();
            return await Task.Run(() => Listingslist);
        }

        public async Task<Listing?> GetListingAsync(int id)
        {
            SqlCommand cmdGetListingById = new SqlCommand("SELECT * FROM Listings WHERE listing_id = " + id, _connection);
            _connection.Open();
            SqlDataReader reader = cmdGetListingById.ExecuteReader();
            while (reader.Read())
            {
                Listing listing = new Listing
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Price = reader.GetDouble(2),
                    City = reader.GetString(3),
                    Description = reader.GetString(4),
                    Address = reader.GetString(5),
                };
                _connection.Close();
                return await Task.Run(() => listing);
            }
            _connection.Close();
            return null;
        }
    }
}