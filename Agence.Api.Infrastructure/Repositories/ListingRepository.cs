using System;
using System.Reflection.PortableExecutable;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

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
                    newListing.Image = reader.GetString(6);
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
                    Image = reader.GetString(6)
                };
                _connection.Close();
                return await Task.Run(() => listing);
            }
            _connection.Close();
            return null;
        }

        public async Task<IActionResult> PostListingAsync(Listing listing)
        {

            using (SqlCommand cmdPostListing = new SqlCommand("INSERT INTO Listings (name, price, city, description, address, image_url) " +
                "VALUES ( @name , @price , @city , @description ,@address, @image)", _connection))
            {
                cmdPostListing.Parameters.AddWithValue("@name", listing.Title);
                cmdPostListing.Parameters.AddWithValue("@price", listing.Price);
                cmdPostListing.Parameters.AddWithValue("@city", listing.City);
                cmdPostListing.Parameters.AddWithValue("@description", listing.Description);
                cmdPostListing.Parameters.AddWithValue("@address", listing.Address);
                cmdPostListing.Parameters.AddWithValue("@image", listing.Image);
                _connection.Open();
                int rowsAffected = cmdPostListing.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return new OkResult();
                }
                else
                {
                    return new BadRequestResult();
                }
            }
        }
    }
}
