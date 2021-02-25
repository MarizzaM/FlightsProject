using FlightsProject;
using Npgsql;
using System;

namespace Runner
{
    class Program
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";

        static bool TestDbConnection()
        {
            try
            {
                using (var my_conn = new NpgsqlConnection(conn_string))
                {
                    my_conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // write error to log file
                return false;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestDbConnection();
        }
    }
}
