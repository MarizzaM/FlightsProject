using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    public class AdminDAOPGSQL : IAdminDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";


        public void Add(Admin a)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO Administrators (First_Name , Last_Name , Level, User_id ) VALUES ('{a.First_Name}', '{a.Last_Name}', {a.Level}, {a.User_id})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{a.First_Name} {a.Last_Name} inserted successfully to table 'Admin'");
            }
        }

        public Admin Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM administrators WHERE administrators.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Admin admin = new Admin
                    {
                        Id = (int)reader["Id"],
                        First_Name  = (string)reader["First_Name"],
                        Last_Name = (string)reader["Last_Name"],
                        Level  = (int)reader["Level"],
                        User_id = (int)reader["User_id"],
                    };

                    return admin;
                }
            }
            return null;
        }
        IList<Admin> admins = new List<Admin>();
        public IList<Admin> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM administrators";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text; 

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Admin admin = new Admin
                    {
                        Id = (int)reader["Id"],
                        First_Name = (string)reader["First_Name"],
                        Last_Name = (string)reader["Last_Name"],
                        Level = (int)reader["Level"],
                        User_id = (int)reader["User_id"],
                    };

                    admins.Add(admin);
                }
            }
            return admins;
        }

        public void Remove(Admin a)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM administrators WHERE administrators.id = {a.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{a.First_Name} {a.Last_Name} has been deleted successfully from table 'Admin'");
            }
        }
        public void Update(Admin a)
        {
            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE  administrators SET Administrators.Id = {a.Id}, " +
                    $"Administrators.First_Name  = '{a.First_Name}', Administrators.Last_Name = '{a.Last_Name}' " +
                    $" Administrators.Level = {a.Level},  Administrators.User_id = {a.User_id} WHERE administrators.id = {a.Id}";
                Console.WriteLine($"{a.First_Name} {a.Last_Name} has been updeted successfully in table 'Admin'");
            }
        }
    }
}
