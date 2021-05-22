using FlightsProject.I_DAO;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class UserDAOPGSQL : IUserDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(User u)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO Users (Username, Password, Email, User_Role) " +
                    $"VALUES ('{u.Username}', '{u.Password}', '{u.Email}', {u.User_Role})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"'{u.Username} inserted successfully to table 'Users'");
            }
        }

        public User Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Email = (string)reader["Email"],
                        User_Role = (int)reader["User_Role"],
                    };
                    return user;
                }
            }
            return null;
        }
        IList<User> users = new List<User>();
        public IList<User> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM users";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Email = (string)reader["Email"],
                        User_Role = (int)reader["User_Role"],
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public User GetByUserName(string username)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM users WHERE users.username = '{username}'";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User
                    {
                        Id = (long)reader["Id"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Email = (string)reader["Email"],
                        User_Role = (int)reader["User_Role"],
                    };
                    return user;
                }
            }
            return null;
        }

        public void Remove(User u)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM Users WHERE Users.id = {u.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{u.Username} has been deleted successfully from table 'Users'");
            }
        }

        public void Update(User u)
        {
            {
                using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
                {
                    my_conn.Open();

                    using var cmd = new NpgsqlCommand();
                    cmd.Connection = my_conn;

                    cmd.CommandText = $"UPDATE  Users SET Users.Id = {u.Id}, " +
                        $"Users.Username  = '{u.Username}', " +
                        $"Users.Password = '{u.Password}' " +
                        $"Users.Email = '{u.Email}',  " +
                        $"Users.User_Role = {u.User_Role} WHERE Users.id = {u.Id}";
                    Console.WriteLine($"'{u.Username} has been updeted successfully in table 'Users'");
                }
            }
        }
    }
}
