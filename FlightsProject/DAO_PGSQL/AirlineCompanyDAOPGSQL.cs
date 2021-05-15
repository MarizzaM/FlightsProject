using FlightsProject.I_DAO;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class AirlineCompanyDAOPGSQL : IAirlineCompanyDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        public void Add(AirlineCompany ac)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO Airline_Companies (Name, countryid, User_Id) VALUES ('{ac.Name}', {ac.Country_Id}, {ac.User_Id})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{ac.Name} inserted successfully to table 'Airline_Companies'");
            }
        }

        public AirlineCompany Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM Airline_Companies WHERE Airline_Companies.Id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AirlineCompany airline = new AirlineCompany
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Country_Id = (int)reader["Country_Id"],
                        User_Id = (int)reader["User_Id"]
                };
                    return airline;
                }
            }
            return null;
        }

        IList<AirlineCompany> airlines = new List<AirlineCompany>();
        public IList<AirlineCompany> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM Airline_Companies";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AirlineCompany airline = new AirlineCompany
                    {
                        Id = (long)reader["Id"],
                        Name = (string)reader["Name"],
                        Country_Id = (int)reader["CountryId"],
                        User_Id = (long)reader["User_Id"]
                    };
                    airlines.Add(airline);
                }
            }
            return airlines;
        }

        public void Remove(AirlineCompany ac)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM Airline_Companies WHERE Airline_Companies.id = {ac.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{ac.Name} has been deleted successfully from table 'Airline_Companies'");
            }
        }

        public void Update(AirlineCompany ac)
        {
            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE  Airline_Companies SET Airline_Companies.Id = {ac.Id}, " +
                    $"Airline_Companies.Name  = '{ac.Name}', Airline_Companies.Country_Id = '{ac.Country_Id}' " +
                    $" Airline_Companies.User_Id = {ac.User_Id}, WHERE Airline_Companies.id = {ac.Id}";
                Console.WriteLine($"{ac.Name} has been updeted successfully in table 'Airline_Companies'");
            }
        }

        public AirlineCompany GetAirlineByUserame(string name)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT Airline_Companies.id, Airline_Companies.name, Airline_Companies.countryid, Airline_Companies.user_id, Users.username " +
                    $"FROM Airline_Companies JOIN Users ON Airline_Companies.User_id = Users.id WHERE Users.username = '{name}' ";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AirlineCompany airline = new AirlineCompany
                    {
                        Id = (long)reader["Id"],
                        NameOfAirline = (string)reader["name"],
                        Country_Id = (int)reader["CountryId"],
                        User_Id = (long)reader["User_Id"],
                        Username = (string)reader["Username"]
                    };
                    return airline;
                }
            }
            return null;
        }

        public IList<AirlineCompany> GetAllAirlinesByCountry(int countryId)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT Airline_Companies.id, Airline_Companies.name as name_of_airline_company, Airline_Companies.countryid, Countries.name as name_of_country, Airline_Companies.user_id " +
                    $"FROM Airline_Companies JOIN Countries ON Airline_Companies.Countryid = Countries.id WHERE Countries.id = {countryId} ";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AirlineCompany airline = new AirlineCompany
                    {
                        Id = (int)reader["Id"],
                        NameOfAirline = (string)reader["name_of_airline_company"],
                        Country_Id = (int)reader["Country_Id"],
                        NameOfCountry = (string)reader["name_of_country"],
                        User_Id = (int)reader["User_Id"]
                    };
                    airlines.Add(airline);
                }
            }
            return airlines;
        }

    }
}
