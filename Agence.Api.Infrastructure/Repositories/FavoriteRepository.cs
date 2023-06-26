using System;
using System.Data.SqlClient;
using System.Reflection;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agence.Api.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private SqlConnection _connection;
        private static IEnumerable<Favorite>? Favorites;
        private string connectionString;

        public FavoriteRepository()
        {
            connectionString = GetConnectionString();
            _connection = new SqlConnection(connectionString);
        }

        static private string GetConnectionString()
        {
            return "Data Source = localhost,1433; Database = MiCasa; Integrated Security = false; User ID = sa; Password = @Reda.2001";
        }

        public async Task<Favorite> getFavoriteByIdAsync(int id)
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
                            return await Task.Run(() => favorite);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<Favorite>> getFavoritesByEmailAsync(string user_email)
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
                        return Favoriteslist;
                    }
                }
            }
        }

        public async Task<IActionResult> postFavoriteAsync(Favorite favorite)
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
                        return await Task.Run(() => new OkResult());
                    }
                    else
                    {
                        return await Task.Run(() => new BadRequestResult());
                    }
                }
            }
        }

        public async Task<IActionResult> deleteFavoriteAsync(int favorite_id)
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
                        return await Task.Run(() => new OkResult());
                    }
                    else
                    {
                        return await Task.Run(() => new NotFoundResult());
                    }
                }
            }
        }

    }
}

