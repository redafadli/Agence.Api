using System;
using System.Data.SqlClient;
using Agence.Api.Application.Repositories;
using Agence.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Agence.Api.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        //private SqlConnection _connection;
        private string? connectionString;
        private readonly IConfiguration _configuration;


        public AppointmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("MyDB");
            //_connection = new SqlConnection(connectionString);
        }

        //static private string GetConnectionString()
        //{
        //    return "Data Source = localhost,1433; Database = MiCasa; Integrated Security = false; User ID = sa; Password = @Reda.2001";
        //}

        public async Task<IActionResult> deleteAppointmentAsync(int appointment_id)
        {
            string sqlQuery = "DELETE FROM Appointments WHERE appointment_id = @appointmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdDeleteFavorite = new SqlCommand(sqlQuery, connection))
                {
                    cmdDeleteFavorite.Parameters.AddWithValue("@appointmentId", appointment_id);

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

        public async Task<IEnumerable<Appointment>> getAppointmentByEmailAsync(string user_email)
        {
            string sqlQuery = "SELECT * FROM Appointments WHERE user_email = '" + user_email + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdGetListings = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = await cmdGetListings.ExecuteReaderAsync())
                    {
                        List<Appointment> Appointmentslist = new List<Appointment>();
                        while (await reader.ReadAsync())
                        {
                            Appointment newAppointment = new Appointment();
                            newAppointment.Id = reader.GetInt32(0);
                            newAppointment.User_email = reader.GetString(1);
                            newAppointment.Appointment_date_time = reader.GetDateTime(2);
                            newAppointment.Listing_id = reader.GetInt32(3);
                            Appointmentslist.Add(newAppointment);
                        }
                        return Appointmentslist;
                    }
                }
            }
        }

        public async Task<Appointment> getAppointmentByIdAsync(int id)
        {
            string sqlQuery = "SELECT * FROM Appointments WHERE appointment_id = " + id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand cmdGetListingById = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = cmdGetListingById.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                Id = reader.GetInt32(0),
                                User_email = reader.GetString(1),
                                Appointment_date_time = reader.GetDateTime(2),
                                Listing_id = reader.GetInt32(3)
                            };
                            return await Task.Run(() => appointment);
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IActionResult> postAppointmentAsync(Appointment appointment)
        {
            string sqlQuery = "INSERT INTO Appointments (user_email, listing_id, date_time) " +
                "VALUES (@user_email , @listing_id, @date_time)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdPostListing = new SqlCommand(sqlQuery, connection))
                {
                    cmdPostListing.Parameters.AddWithValue("@user_email", appointment.User_email);
                    cmdPostListing.Parameters.AddWithValue("@listing_id", appointment.Listing_id);
                    cmdPostListing.Parameters.AddWithValue("@date_time", appointment.Appointment_date_time);

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

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            string sqlQuery = "SELECT * FROM Appointments";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmdGetListings = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = await cmdGetListings.ExecuteReaderAsync())
                    {
                        List<Appointment> Appointmentslist = new List<Appointment>();
                        while (await reader.ReadAsync())
                        {
                            Appointment newAppointment = new Appointment();
                            newAppointment.Id = reader.GetInt32(0);
                            newAppointment.User_email = reader.GetString(1);
                            newAppointment.Appointment_date_time = reader.GetDateTime(2);
                            newAppointment.Listing_id = reader.GetInt32(3);
                            Appointmentslist.Add(newAppointment);
                        }
                        return Appointmentslist;
                    }
                }
            }
        }
    }
}

