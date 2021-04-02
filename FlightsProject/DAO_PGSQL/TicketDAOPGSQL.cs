using FlightsProject.I_DAO;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class TicketDAOPGSQL : ITicketDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(Ticket t)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO tickets (flight_id, customer_id) VALUES ({t.Id_Flight}, {t.Id_Customer})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{t.Id_Flight} {t.Id_Customer} inserted successfully to table 'Admin'");
            }
        }

        public Ticket Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM tickets WHERE tickets.id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = new Ticket
                    {
                        Id = (int)reader["Id"],
                        Id_Flight = (int)reader["Id_Flight"],
                        Id_Customer = (int)reader["Id_Customer"]
                    };
                    return ticket;
                }
            }
            return null;
        }
        IList<Ticket> tickets = new List<Ticket>();
        public IList<Ticket> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM tickets";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ticket ticket = new Ticket
                    {
                        Id = (int)reader["Id"],
                        Id_Flight = (int)reader["Id_Flight"],
                        Id_Customer = (int)reader["Id_Customer"]
                    };
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        public void Remove(Ticket t)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM tickets WHERE tickets.id = {t.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"ticket #{t.Id} has been deleted successfully from table 'Tickets'");
            }
        }

        public void Update(Ticket t)
        {
            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE tickets SET tickets.Id = {t.Id}, " +
                    $"tickets.Id_Flight  = {t.Id_Flight}, " +
                    $"tickets.Id_Customer = {t.Id_Customer}";
                Console.WriteLine($"ticket #{t.Id} has been updeted successfully in table 'Tickets'");
            }
        }
        public void copyToTicketHistory()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"Insert into tickets_history(id_flight, id_customer) " +
                    $"SELECT* from tickets join flights on tickets.id_flight = flights.id where flights.landing_time < (NOW() + interval '3 hour'); " +
                    $"DELETE FROM Tickets using flights where flights.landing_time < (NOW() + interval '3 hour')";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Tickets inserted successfully to table 'TicketHistory'");
            }
        }
    }
}
