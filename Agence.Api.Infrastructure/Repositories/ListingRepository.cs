using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Infrastructure.Repositories
{
    public class ListingRepository : IListingRepository
    {

        private SqlConnection _connection;
        private static IEnumerable<Listing>? Listings;
        private string? connectionString;
        private readonly IConfiguration _configuration;

        public ListingRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(connectionString);
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("MyDB");
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
                        List<Listing> listings = new List<Listing>();

                        while (await reader.ReadAsync())
                        {
                            Listing listing = new Listing
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDouble(2),
                                City = reader.GetString(3),
                                Description = reader.GetString(4),
                                Address = reader.GetString(5),
                                ImageUrls = reader.GetString(6).Split(',').ToList()
                            };

                            listings.Add(listing);
                        }

                        return listings;
                    }
                }
            }
        }

        public async Task<IActionResult> DeleteListingByIdAsync(int id)
        {
            string sqlDeleteListing = "DELETE FROM Listings WHERE listing_id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdDeleteListing = new SqlCommand(sqlDeleteListing, connection))
                {
                    cmdDeleteListing.Parameters.AddWithValue("@id", id);

                    int rowsAffected = await cmdDeleteListing.ExecuteNonQueryAsync();

                    return new OkResult();
                }
            }
        }


        public async Task<Listing> GetListingByIdAsync(int id)
        {
            string sqlQuery = "SELECT L.listing_id, L.name, L.price, L.state, L.city, L.description, L.address, L.image_urls " +
                              "FROM Listings L " +
                              "WHERE L.listing_id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdGetListingById = new SqlCommand(sqlQuery, connection))
                {
                    cmdGetListingById.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = await cmdGetListingById.ExecuteReaderAsync())
                    {
                        Listing listing = null;

                        while (await reader.ReadAsync())
                        {
                            if (listing == null)
                            {
                                listing = new Listing
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Price = reader.GetDouble(2),
                                    State = reader.GetString(3),
                                    City = reader.GetString(4),
                                    Description = reader.GetString(5),
                                    Address = reader.GetString(6),
                                    ImageUrls = new List<string>()
                                };

                                // Split and add image URLs
                                string imageUrlsString = reader.IsDBNull(7) ? null : reader.GetString(7);
                                if (!string.IsNullOrEmpty(imageUrlsString))
                                {
                                    listing.ImageUrls.AddRange(imageUrlsString.Split(','));
                                }
                            }
                        }

                        return listing;
                    }
                }
            }
        }




        public async Task<IActionResult> PostListingAsync(Listing listing)
        {
            string sqlInsertListing = "INSERT INTO Listings (name, price, state, city, description, address, image_urls) " +
                "VALUES (@name, @price, @state, @city, @description, @address, @imageUrls)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdInsertListing = new SqlCommand(sqlInsertListing, connection))
                {
                    cmdInsertListing.Parameters.AddWithValue("@name", listing.Name);
                    cmdInsertListing.Parameters.AddWithValue("@price", listing.Price);
                    cmdInsertListing.Parameters.AddWithValue("@state", listing.State);
                    cmdInsertListing.Parameters.AddWithValue("@city", listing.City);
                    cmdInsertListing.Parameters.AddWithValue("@description", listing.Description);
                    cmdInsertListing.Parameters.AddWithValue("@address", listing.Address);
                    cmdInsertListing.Parameters.AddWithValue("@imageUrls", string.Join(",", listing.ImageUrls));

                    await cmdInsertListing.ExecuteNonQueryAsync();

                    return new OkResult();
                }
            }
        }


        public async Task<IActionResult> PutListingAsync(Listing listing)
        {
            string sqlQuery = "UPDATE Listings SET name = @name, price = @price, state = @state, city = @city, description = @description, address = @address" +
                "WHERE listing_id = " + listing.Id;
            using (SqlCommand cmdPutListing = new SqlCommand(sqlQuery, _connection))
            {
                cmdPutListing.Parameters.AddWithValue("@name", listing.Name);
                cmdPutListing.Parameters.AddWithValue("@price", listing.Price);
                cmdPutListing.Parameters.AddWithValue("@state", listing.State);
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
