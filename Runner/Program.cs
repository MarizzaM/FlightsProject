using FlightsProject;
using Npgsql;
using System;
using System.Collections.Generic;

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
            TestDbConnection();
            CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();

            Country countryDominicana = new Country
            {
                Name = "Dominicana"
            };

            //countryDAOPGSQL.Add(countryDominicana);



            //var countries = countryDAOPGSQL.GetAll();

            //foreach (var c in countries)
            //{
            //    var id = ((dynamic)c).Id;
            //    var name = ((dynamic)c).Name;

            //    Console.WriteLine($"{id} {name}");
            //}

            //Console.WriteLine(countryDAOPGSQL.Get(1));

            countryDAOPGSQL.Remove(countryDominicana);

            //countryDAOPGSQL.Update(countryDominicana);

            var countries = countryDAOPGSQL.GetAll();

            foreach (var c in countries)
            {
                var id = ((dynamic)c).Id;
                var name = ((dynamic)c).Name;

                Console.WriteLine($"{id} {name}");
            }

        }
    }
}
