using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace TestFlightsProject
{
    [TestClass]
    public class LoggedInAdministratorFacadeTest
    {
        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";

        public void DeleteAllData()
        {
            using (var my_conn = new NpgsqlConnection(conn_string_test))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                cmd.CommandText = $"DELETE FROM administrators; DELETE FROM users;";


                //cmd.CommandText = $"DELETE FROM administrators; DELETE FROM airline_companies; DELETE FROM customers; " +
                //    $"DELETE FROM flights; DELETE FROM tickets; DELETE FROM users;";
                cmd.ExecuteNonQuery();
               
            }
        }
        

        [TestMethod]
        public void CreateAdminTest()
        {


            UserDAOPGSQL user = new UserDAOPGSQL();
            AdminDAOPGSQL admin = new AdminDAOPGSQL();

            DeleteAllData();

            User manager1 = new User(TestData.CreateAdminTest_username,
                TestData.CreateAdminTest_password,
                TestData.CreateAdminTest_email,
                TestData.CreateAdminTest_user_role);
            user.Add(manager1);

            User u = user.GetByUserName(TestData.CreateAdminTest_username);

            Admin admin1 = new Admin(TestData.CreateAdminTest_firstName,
                TestData.CreateAdminTest_lastName,
                TestData.CreateAdminTest_level,
                (long)u.Id);

            admin.Add(admin1);

            Assert.AreNotEqual(admin, 0);

        }
        [TestMethod]
        public void CreateNewAirlineTest()
        {

        }
        [TestMethod]
        public void CreateNewCustomerTest()
        {
        }
        [TestMethod]
        public void GetAllCustomersTest()
        {
        }
        [TestMethod]
        public void RemoveAdminTest()
        {
        }
        [TestMethod]
        public void RemoveAirlineTest()
        {
        }
        [TestMethod]
        public void RemoveCustomerTest()
        {
        }
        [TestMethod]
        public void UpdateAdminTest()
        {
        }
        [TestMethod]
        public void UpdateAirlineDetailsTest()
        {
        }
        [TestMethod]
        public void UpdateCustomerDetailsTest()
        {
        }
    }
}
