using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Data;

namespace RazorDb.Services
{
    public class RoomService : IRoomService
    {
        private String connectionString = Secret.ConnectionString;
        private string quereyString = "SELECT Room_No,Hotel_No,Types,Price from Room";
        private string insertsql = "INSERT INTO Room Values(@RoomNr,@HotelNr,@Types,@Price)";
        private string deletesql = "DELETE from Room WHERE Room_No=@RoomNr AND Hotel_No=@HotelNr";
        private string updateRoomsql = "UPDATE Room SET Types = @Types,Price = @Price WHERE Hotel_No = @HotelNr AND Room_No=@RoomNr";
        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertsql, connection);
                    command.Parameters.AddWithValue("@RoomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNr", room.HotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    Thread.Sleep(1000);
                    //int noOfRows = command.ExecuteNonQuery();

                    //return noOfRows == 1;

                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally
                {

                }
                return true;
            }
        }

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr) //Sletter ikke
        {
            Room? room = await GetRoomFromIdAsync(roomNr, hotelNr);
            if (room == null)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deletesql, connection);
                    command.Parameters.AddWithValue("@RoomNr", roomNr);
                    command.Parameters.AddWithValue("@HotelNr", hotelNr);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    Thread.Sleep(1000);
                    if (noOfRows == 0)
                    {
                        return null;
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    room = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    room = null;
                }
                finally
                {

                }
                return room;
            }
        }

        public async Task<List<Room>> GetAllRoomFromHotelNoAsync(int hotelNr)
        {
                List<Room> rooms = new List<Room>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(quereyString + " WHERE Hotel_No = @HotelNr", connection);
                        command.Parameters.AddWithValue("@HotelNr", hotelNr);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        Thread.Sleep(1000);
                        while (await reader.ReadAsync())
                        {
                            int roomNo = reader.GetInt32("Room_No");
                            char type = reader.GetString("Types")[0];
                            double price = reader.GetDouble("Price");
                            hotelNr = reader.GetInt32("Hotel_No");
                            Room room = new Room(roomNo, type, price, hotelNr);
                            rooms.Add(room);
                        }
                        reader.Close();
                    }
                    catch (SqlException sqlExp)
                    {
                        Console.WriteLine("Database error: " + sqlExp.Message);
                        throw sqlExp;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("General error: " + ex.Message);
                    throw ex;
                    }
                }

                return rooms;
        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(quereyString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32("Room_No");
                        char type = reader.GetString(reader.GetOrdinal("Types"))[0];
                        double price = reader.GetDouble("Price");
                        int hotelNr = reader.GetInt32("Hotel_No");
                        Room room = new Room(roomNo, type, price, hotelNr);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }

            return rooms;
        }


        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Room room = null;
                try
                {
                    SqlCommand command = new SqlCommand(quereyString + " where room_No=@Room_No and Hotel_no=@ID ", connection);
                    command.Parameters.AddWithValue("@Room_No", roomNr);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        int rNr = reader.GetInt32("Room_No");
                        int hNr = reader.GetInt32("Hotel_No");
                        char type = reader.GetString("Types")[0];
                        double price = reader.GetDouble("Price");
                        room = new Room(rNr, type, price, hotelNr);
                    }
                    reader.Close();
                    return room;

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
                return room;
            }
            return null;
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateRoomsql, connection);
                    command.Parameters.AddWithValue("@RoomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@HotelNr", room.HotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();

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
                return true;
            }
        }

        public async Task<List<Room>> GetAllRoomFromPrice(int hotelNr, double price)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(quereyString + " WHERE Hotel_No = @HotelNr AND Price=@Price", connection);
                    command.Parameters.AddWithValue("@HotelNr", hotelNr);
                    command.Parameters.AddWithValue("@Price", price);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32("Room_No");
                        char type = reader.GetString("Types")[0];
                        price = reader.GetDouble("Price");
                        hotelNr = reader.GetInt32("Hotel_No");
                        Room room = new Room(roomNo, type, price, hotelNr);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }

            return rooms;
        }
    }
}
