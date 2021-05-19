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
    public class LoggedInCustomerFacadeTest
    {
        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        public void DeleteAllData()
        {
            using (var my_conn = new NpgsqlConnection(conn_string_test))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();

                cmd.Connection = my_conn;
                cmd.CommandText = $"DELETE FROM flights; DELETE FROM airline_companies;";
                cmd.ExecuteNonQuery();
            }
        }
        LoginService loginService = new LoginService();
        LoginToken<Customer> loginToken = new LoginToken<Customer>();

        public User CreateCustomerUserForTest()
        {
            User customerUser = new User
            {
                Username = TestData.LoggedInCustomerFacade_CreateCustomerUser_Username,
                Password = TestData.LoggedInCustomerFacade_CreateCustomerUser_Password,
                Email = TestData.LoggedInCustomerFacade_CreateCustomerUser_Email,
                User_Role = TestData.LoggedInCustomerFacade_CreateCustomerUser_User_Role
            };
            return customerUser;
        }
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();


        //LoggedInCustomerFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Customer>(loginToken) as LoggedInCustomerFacade;



        [TestMethod]
        public void CancelTicketTest()
        {
        }
        [TestMethod]
        public void GetAllMyFlightsTest()
        {
        }
        [TestMethod]
        public void PurchaseTicketTest()
        {
        }
    }
}
