using FlightsProject.I_DAO;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class CustomerDAOPGSQL : ICustomerDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        public void Add(Customer c)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"INSERT INTO Customers (First_Name , Last_Name , Address, Phone_No, Credit_Card_No, User_Id) " +
                    $"VALUES ('{c.First_Name}', '{c.Last_Name}', '{c.Address}', '{c.Phone_No}', '{c.Credit_Card_No}', {c.User_Id})";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{c.First_Name} {c.Last_Name} inserted successfully to table 'Customers'");
            }
        }

        public Customer Get(int id)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = $"SELECT * FROM Customers WHERE Customers.Id = {id}";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = (long)reader["Id"],
                        First_Name = (string)reader["First_Name"],
                        Last_Name = (string)reader["Last_Name"],
                        Address = (string)reader["Address"],
                        Phone_No = (string)reader["Phone_No"],
                        Credit_Card_No = (string)reader["Credit_Card_No"],
                        User_Id = (long)reader["User_Id"]
                };
                    return customer;
                }
            }
            return null;
        }
        IList<Customer> customers = new List<Customer>();
        public IList<Customer> GetAll()
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();
                string query = "SELECT * FROM Customers";

                NpgsqlCommand command = new NpgsqlCommand(query, my_conn);
                command.CommandType = System.Data.CommandType.Text;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = (long)reader["Id"],
                        First_Name = (string)reader["First_Name"],
                        Last_Name = (string)reader["Last_Name"],
                        Address = (string)reader["Address"],
                        Phone_No = (string)reader["Phone_No"],
                        Credit_Card_No = (string)reader["Credit_Card_No"],
                        User_Id = (long)reader["User_Id"]
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public Customer GetGetCustomerByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer c)
        {
            using (var my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM customers WHERE customers.id = {c.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{c.First_Name} {c.Last_Name} has been deleted successfully from table 'Customers'");
            }
        }

        public void Update(Customer c)
        {
            using (NpgsqlConnection my_conn = new NpgsqlConnection(conn_string))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"UPDATE customers SET " +
                    $"First_Name  = '{c.First_Name}', " +
                    $"Last_Name = '{c.Last_Name}', " +
                    $"Address = '{c.Address}',  " +
                    $"Phone_No = '{c.Phone_No}', " +
                    $"Credit_Card_No = '{c.Credit_Card_No}' " +
                    $"WHERE Customers.id = {c.Id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{c.First_Name} {c.Last_Name} has been updeted successfully in table 'Customers'");
            }
        }
    }
}
