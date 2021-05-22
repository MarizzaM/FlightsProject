using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    public class CountryDAOPGSQL : ICountryDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";

        public void Add(Country c)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO countries (name) VALUES ('{c.Name}')";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{c.Name} inserted successfully to table 'Country'");
            }
        }

        public Country Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM countries WHERE countries.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text; // this is default

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    };

                    return country;
                }
            }
            return null;
        }

        IList<Country> countries = new List<Country>();
        public IList<Country> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM countries";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text; // this is default

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    };

                    countries.Add(country);
                }
            }
            return countries;
        }
        public void Remove(Country c)
        {

            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM countries WHERE countries.name = '{c.Name}'";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{c.Name} has been deleted successfully from table 'Country'");

            }
        }

        public void Update(Country c)
        {

            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE  countries SET countries.id = {c.Id}, countries.name = '{c.Name}'  WHERE countries.name = '{c.Id}'";
                Console.WriteLine($"{c.Name} has been updeted successfully in table 'Country'");
            }
        }
    }
}
