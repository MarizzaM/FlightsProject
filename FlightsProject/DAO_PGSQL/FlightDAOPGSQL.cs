using FlightsProject.I_DAO;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class FlightDAOPGSQL : IFlightDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(Flight f)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO flights (airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, tickets_remaining) " +
                    $"VALUES ('{f.Airline_Company_Id}', '{f.Origin_Country_Id}', '{f.Destination_Country_Id}', {f.Departure_Time}, {f.Landing_Time}, {f.Tickets_Remaining})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Flight inserted successfully to table 'Flights'");
            }
        }

        public Flight Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM flights WHERE flights.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };
                    return flight;
                }
            }
            return null;
        }
        IList<Flight> flights = new List<Flight>();
        public IList<Flight> GetAll()
        {
            
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM flights";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };

                    flights.Add(flight);
                }
            }
            return flights;
        }
        public void Remove(Flight f)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM flights WHERE flights.id = {f.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"flight has been deleted successfully from table 'Flight'");
            }
        }

        public void Update(Flight f)
        {
            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE flights SET flights.Id = {f.Id}, " +
                    $"flights.Airline_Company_Id  = {f.Airline_Company_Id}, " +
                    $"flights.Origin_Country_Id = {f.Origin_Country_Id}, " +
                    $"flights.Destination_Country_Id = {f.Destination_Country_Id},  " +
                    $"flights.Departure_Time = {f.Departure_Time}, " +
                    $"flights.Landing_Time = {f.Landing_Time},  " +
                    $"flights.Tickets_Remaining = {f.Tickets_Remaining} " +
                    $"WHERE administrators.id = {f.Id}";
                Console.WriteLine($"flight #{f.Id} has been updeted successfully in table 'Flight'");
            }
        }

        Dictionary<Flight, int> IFlightDAO.GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights_vacancy = new Dictionary<Flight, int>();
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM flights where Tickets_Remaining > 0";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };

                    flights_vacancy.Add(flight, (int)reader["Tickets_Remaining"]);
                }
            }
            return flights_vacancy;
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            IList<Flight> flights = new List<Flight>();
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT flights.Id, flights.Airline_Company_Id, flights.Origin_Country_Id, Countries.name as name_of_origin_country, flights.Destination_Country_Id,  " +
                    $"flights.Departure_Time, flights.Landing_Time, flights.Tickets_Remaining FROM flights join countries on flights.Origin_Country_Id = countries.id where countries.id = {countryCode}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        NameOfOriginCountry = (string)reader["name_of_origin_country"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };

                    flights.Add(flight);
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            IList<Flight> flights = new List<Flight>();
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT flights.Id, flights.Airline_Company_Id, flights.Origin_Country_Id, flights.Destination_Country_Id, Countries.name as name_of_destination_country, " +
                    $"flights.Departure_Time, flights.Landing_Time, flights.Tickets_Remaining FROM flights join countries on flights.Destination_Country_Id = countries.id where countries.id = {countryCode}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        NameOfOriginCountry = (string)reader["name_of_origin_country"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };

                    flights.Add(flight);
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            IList<Flight> flights = new List<Flight>();
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM flights WHERE flights.Departure_Time = {departureDate}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };
                    flights.Add(flight);
                }
            }
            return flights;
        }

        IList<Flight> IFlightDAO.GetFlightsByLandingDate(DateTime landingDate)
        {
            IList<Flight> flights = new List<Flight>();
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM flights WHERE flights.Landing_Time = {landingDate}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };

                    flights.Add(flight);
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            TicketDAOPGSQL t = new TicketDAOPGSQL();
            List<Ticket> tickets = (List<Ticket>)t.GetAll();
            tickets.ForEach(ticket =>
            {
                if (ticket.Id_Customer == customer.Id)
                {
                    flights.Add(Get(ticket.Id_Flight));
                }
            });
            return flights;
        }

        public Flight GetFlightsById(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM flights WHERE flights.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Flight flight = new Flight
                    {
                        Id = (int)reader["Id"],
                        Airline_Company_Id = (int)reader["Airline_Company_Id"],
                        Origin_Country_Id = (int)reader["Origin_Country_Id"],
                        Destination_Country_Id = (int)reader["Destination_Country_Id"],
                        Departure_Time = (DateTime)reader["Departure_Time"],
                        Landing_Time = (DateTime)reader["Landing_Time"],
                        Tickets_Remaining = (int)reader["Tickets_Remaining"]
                    };
                    return flight;
                }
            }
            return null;
        }

        public void copyToFlighsHistory()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO flighs_history(Airline_Company_Id, Origin_Country_Id, Destination_Country_Id, Departure_Time, Landing_Time, Tickets_Remaining) " +
                    $"SELECT* from flights where flights.landing_time < (NOW() + interval '3 hour'); " +
                    $"DELETE from flights where flights.landing_time < (NOW() + interval '3 hour');";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Flights inserted successfully to table 'Flights'");
            }
        }
    }
}
