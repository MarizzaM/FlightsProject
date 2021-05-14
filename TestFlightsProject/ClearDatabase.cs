using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightsProject
{
    public class ClearDatabase
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        public void DeleteAllData ()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM administrators; DELETE FROM airline_companies; DELETE FROM customers; " +
                    $"DELETE FROM flights; DELETE FROM tickets; DELETE FROM users;";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"All data has been deleted successfully from tables'");
            }
        }
    }
}
