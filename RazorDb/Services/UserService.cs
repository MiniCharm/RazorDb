using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Data;

namespace RazorDb.Services
{
    public class UserService : IUSerService
    {
        //private String connectionString = Secret.ConnectionString;
        //private string quereyString = "SELECT UserName,Password from Users";
        //private string insertsql = "INSERT INTO Users Values(@UserName,@Password)";
        //private string deletesql = "DELETE from Users WHERE UserName=@UserName";
        //public Task<bool> CreateUserAsync(User user, string userNAme)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<User> DeleteUserAsync(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<List<User>> GetAllUSersAsync()
        //{
        //    List<User> users = new List<User>();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(quereyString, connection);
        //            await command.Connection.OpenAsync();
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            Thread.Sleep(1000);
        //            while (await reader.ReadAsync())
        //            {
        //                string userName = reader.GetString("UserName");
        //                string password = reader.GetString("Password");
        //                User user = new User(userName, password);
        //                users.Add(user);
        //            }
        //            reader.Close();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error: " + sqlExp.Message);
        //            throw sqlExp;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("General error: " + ex.Message);
        //            throw ex;
        //        }
        //    }

        //    return users;
        //}

        //public async Task<User> GetUserFromUserNameAsync(string userName)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        User user = null;
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(quereyString + " where UserName=@UserName", connection);
        //            command.Parameters.AddWithValue("@UserName", userName);
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            if (reader.Read())
        //            {
        //                userName = reader.GetString("UserName");
        //                string password = reader.GetString("Password");
        //                user = new User(userName, password);
        //            }
        //            reader.Close();
        //            return user;
        //        }

        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }

        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }

        //        finally
        //        {

        //        }
        //        return user;
        //    }
        //}

        //public async Task<User> VerifyUserAsync(string userName, string password)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        User user = null;
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(quereyString, connection);
        //            await command.Connection.OpenAsync();
        //            command.Parameters.AddWithValue("@UserName", userName);
        //            command.Parameters.AddWithValue("@Password", password);
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            if (reader.Read())
        //            {
        //                userName = reader.GetString("UserName");
        //                password = reader.GetString("Password");
        //                user = new User(userName, password);
        //            }
        //            reader.Close();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error: " + sqlExp.Message);
        //            throw sqlExp;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("General error: " + ex.Message);
        //            throw ex;
        //        }
        //        return user;
        //    }
        //}
    }
}
