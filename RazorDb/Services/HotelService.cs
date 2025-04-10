using Microsoft.Data.SqlClient;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Data;

namespace RazorDb.Services
{
    public class HotelService:IHotelServiceAsync
    {
        private String connectionString = Secret.ConnectionString;
        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";
        private string insertSql = "Insert INTO Hotel Values(@ID, @Navn, @Adresse)";
        private string deleteSql = "Delete from Hotel where Hotel_No = @ID";
        private string updateHotel = "UPDATE Hotel SET Name = @Navn , Address = @Adresse where Hotel_No = @ID";

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    //int noOfRows = command.ExecuteNonQuery();

                    //return noOfRows == 1;

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return true;
            }
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel? hotel = await GetHotelFromIdAsync(hotelNr);
            if (hotel == null)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(deleteSql, connection);
                    cmd.Parameters.AddWithValue("@Id", hotelNr);
                    await cmd.Connection.OpenAsync();

                    int noOfRows = await cmd.ExecuteNonQueryAsync();
                    Thread.Sleep(1000);
                    if (noOfRows == 0)
                    {
                        return null;
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    hotel = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    hotel = null;
                }
                finally
                {

                }
                return hotel;
            }

            return null;
        }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return hoteller;

        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Hotel hotel = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where hotel_no=@ID ", connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    if (reader.Read())
                    {
                        int hNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        hotel = new Hotel(hNr, hotelNavn, hotelAdr);
                    }
                    reader.Close();
                    return hotel;

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return hotel;
            }
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Hotel> _foundHotels = new List<Hotel>();

                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where Name like @Search", connection);
                    command.Parameters.AddWithValue("@Search", "%" + name + "%");
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        _foundHotels.Add(hotel);
                    }
                    reader.Close();
                    return _foundHotels;
                }

                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }

                finally
                {

                }
                return _foundHotels;
            }
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            //Opdater ikke ID,
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateHotel, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    Thread.Sleep(1000);
                }


                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    return false;
                }
                finally
                {

                }
            }
            return true;
        }
    }
}
