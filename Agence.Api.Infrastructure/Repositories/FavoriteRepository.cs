﻿using System.Data.SqlClient;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Agence.Api.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private SqlConnection _connection;
        private string? connectionString;
        private readonly IConfiguration _configuration;

        public FavoriteRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(connectionString);
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("MyDB");
        }

        public async Task<Favorite> GetFavoriteByIdAsync(int id)
        {
            string sqlQuery = "SELECT * FROM Favorites WHERE favorite_id = " + id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmdGetListingById = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = cmdGetListingById.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Favorite favorite = new Favorite
                            {
                                Id = reader.GetInt32(0),
                                User_email = reader.GetString(1),
                                Listing_id = reader.GetInt32(2)
                            };
                            Log.Information("Successfully got the favorite by Id");
                            return await Task.Run(() => favorite);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByEmailAsync(string user_email)
        {
            string sqlQuery = "SELECT * FROM Favorites WHERE user_email = '" + user_email + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdGetListings = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = await cmdGetListings.ExecuteReaderAsync())
                    {
                        List<Favorite> Favoriteslist = new List<Favorite>();
                        while (await reader.ReadAsync())
                        {
                            Favorite newFavorite = new Favorite();
                            newFavorite.Id = reader.GetInt32(0);
                            newFavorite.User_email = reader.GetString(1);
                            newFavorite.Listing_id = reader.GetInt32(2);
                            Favoriteslist.Add(newFavorite);
                        }
                        Log.Information("Got the favorites of the user successfully");
                        return Favoriteslist;
                    }
                }
            }
        }

        public async Task<IActionResult> PostFavoriteAsync(Favorite favorite)
        {
            string sqlQuery = "INSERT INTO Favorites (user_email, listing_id) " +
                "VALUES (@user_email , @listing_id)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdPostListing = new SqlCommand(sqlQuery, connection))
                {
                    cmdPostListing.Parameters.AddWithValue("@user_email", favorite.User_email);
                    cmdPostListing.Parameters.AddWithValue("@listing_id", favorite.Listing_id);

                    await connection.OpenAsync();
                    int rowsAffected = await cmdPostListing.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        Log.Information("Favorite added successfully");
                        return await Task.Run(() => new OkResult());
                    }
                    else
                    {
                        Log.Error("Error adding the favorite to the database");
                        return await Task.Run(() => new BadRequestResult());
                    }
                }
            }
        }

        public async Task<IActionResult> DeleteFavoriteAsync(int favorite_id)
        {
            string sqlQuery = "DELETE FROM Favorites WHERE favorite_id = @favoriteId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdDeleteFavorite = new SqlCommand(sqlQuery, connection))
                {
                    cmdDeleteFavorite.Parameters.AddWithValue("@favoriteId", favorite_id);

                    await connection.OpenAsync();
                    int rowsAffected = await cmdDeleteFavorite.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        Log.Information("Favorite deleted successfully");
                        return await Task.Run(() => new OkResult());
                    }
                    else
                    {
                        Log.Error("Error deleting the favorite to the database");
                        return await Task.Run(() => new NotFoundResult());
                    }
                }
            }
        }

    }
}

