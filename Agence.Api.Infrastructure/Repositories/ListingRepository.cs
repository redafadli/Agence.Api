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
using System.Data.Common;

namespace Agence.Api.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {

        private SqlConnection _connection;
        private static IEnumerable<Listing>? Listings;
        private string connectionString;

        public ListingRepository()
        {
            connectionString = GetConnectionString();
            _connection = new SqlConnection(connectionString);
        }

        static private string GetConnectionString()
        {
            return "Data Source = localhost,1433; Database = MiCasa; Integrated Security = false; User ID = sa; Password = @Reda.2001";
        }

        public async Task<IEnumerable<Listing>> SearchListingsAsync(string term)
        {
            return await Task.Run(() => Listings.Where(l => l.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync()
        {
            string sqlQuery = "SELECT * FROM Listings";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdGetListings = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = await cmdGetListings.ExecuteReaderAsync())
                    {
                        List<Listing> Listingslist = new List<Listing>();
                        while (await reader.ReadAsync())
                        {
                            Listing newListing = new Listing();
                            newListing.Id = reader.GetInt32(0);
                            newListing.Name = reader.GetString(1);
                            newListing.Price = reader.GetDouble(2);
                            newListing.City = reader.GetString(3);
                            newListing.Description = reader.GetString(4);
                            newListing.Address = reader.GetString(5);
                            newListing.Image = reader.GetString(6);
                            Listingslist.Add(newListing);
                        }
                        return Listingslist;
                    }
                }
            }
        }


        public async Task<Listing> GetListingByIdAsync(int id)
        {
            string sqlQuery = "SELECT * FROM Listings WHERE listing_id = " + id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmdGetListingById = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = cmdGetListingById.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Listing listing = new Listing
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDouble(2),
                                City = reader.GetString(3),
                                Description = reader.GetString(4),
                                Address = reader.GetString(5),
                                Image = reader.GetString(6)
                            };
                            return await Task.Run(() => listing);
                        }
                        return null;
                    }
                }
            }
        }


        public async Task<IActionResult> PostListingAsync(Listing listing)
        {
            string sqlQuery = "INSERT INTO Listings (name, price, city, description, address, image_url) " +
                "VALUES (@name , @price , @city , @description ,@address, @image)";
            using (SqlCommand cmdPostListing = new SqlCommand(sqlQuery, _connection))
            {
                cmdPostListing.Parameters.AddWithValue("@name", listing.Name);
                cmdPostListing.Parameters.AddWithValue("@price", listing.Price);
                cmdPostListing.Parameters.AddWithValue("@city", listing.City);
                cmdPostListing.Parameters.AddWithValue("@description", listing.Description);
                cmdPostListing.Parameters.AddWithValue("@address", listing.Address);
                cmdPostListing.Parameters.AddWithValue("@image", listing.Image);
                _connection.Open();
                int rowsAffected = cmdPostListing.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return await Task.Run(() => new OkResult());
                }
                else
                {
                    return await Task.Run(() => new BadRequestResult());
                }
            }
        }

        public async Task<IActionResult> PutListingAsync(Listing listing)
        {
            string sqlQuery = "UPDATE Listings SET name = @name, price = @price, city = @city, description = @description, address = @address" +
                "WHERE listing_id = " + listing.Id;
            using (SqlCommand cmdPutListing = new SqlCommand(sqlQuery, _connection))
            {
                cmdPutListing.Parameters.AddWithValue("@name", listing.Name);
                cmdPutListing.Parameters.AddWithValue("@price", listing.Price);
                cmdPutListing.Parameters.AddWithValue("@city", listing.City);
                cmdPutListing.Parameters.AddWithValue("@description", listing.Description);
                cmdPutListing.Parameters.AddWithValue("@address", listing.Address);
                _connection.Open();
                int rowsAffected = cmdPutListing.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return await Task.Run(() => new OkResult());
                }
                else
                {
                    return await Task.Run(() => new BadRequestResult());
                }
            }
        }
    }
}
