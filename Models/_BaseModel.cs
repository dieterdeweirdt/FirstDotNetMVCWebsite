using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace DotNet.Models;
public class BaseModel : IDisposable
{
    private static readonly string connectionString = "Server=localhost;Database=3306;Uid=root;Pwd=secret;Database=gardenblog;";
    //private static MySqlConnection connection;

    public BaseModel()
    {
        //connection = new MySqlConnection(connectionString);
        //connection.Open();
    }

    protected static IEnumerable<T> GetAll<T>(string query, Func<IDataRecord, T> map)
    {

        Debug.WriteLine("GetAll");

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                List<T> result = new List<T>();
                while (reader.Read())
                {
                    result.Add(map(reader));
                }
                return result;
            }

            connection.Close();

        }
    }
    
    protected static T GetById<T>(string query, int id, Func<IDataRecord, T> map)
    {
       using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            using (MySqlCommand command = new MySqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return map(reader);
                    }
                    return default(T);
                }
                }

            connection.Close();

        }
    }

    public void Dispose()
    {
        /*if (connection != null)
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }*/
    }
}