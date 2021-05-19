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

        public void foo() {
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;
        }

        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        static ILoginService loginService = new LoginService();

        
        public void DeleteAllData()
        {
            using (var my_conn = new NpgsqlConnection(conn_string_test))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();

                cmd.Connection = my_conn;
                cmd.CommandText = $"DELETE FROM flights; " +
                    $"DELETE FROM customers; " +
                    $"DELETE FROM airline_companies; " +
                    $"DELETE FROM administrators; " +
                    $"DELETE FROM users; ";
                cmd.ExecuteNonQuery();
            }
        }


        CustomerDAOPGSQL customerDAOPGSQL = new CustomerDAOPGSQL();
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
        AdminDAOPGSQL adminDAOPGSQL = new AdminDAOPGSQL();



        public User CreateAirlineUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.AnonymouseFacade_CreateAirlineUser_Username,
                Password = TestData.AnonymouseFacade_CreateAirlineUser_Password,
                Email = TestData.AnonymouseFacade_CreateAirlineUser_Email,
                User_Role = TestData.AnonymouseFacade_CreateAirlineUser_User_Role
            };
            return airlineUser;
        }




        public AirlineCompany CreateAirlineCompanyForTest()
        {
            userDAOPGSQL.Add(CreateAirlineUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.AnonymouseFacade_CreateAirlineUser_Username);
            TestData.AnonymouseFacade_CreateAirlineUser_Id = (int)u.Id;

            AirlineCompany airlineCompany = new AirlineCompany
            {
                Name = TestData.AnonymouseFacade_CreateAirlineCompany_Name,
                Country_Id = TestData.AnonymouseFacade_CreateAirlineCompany_Country_Id,
                User_Id = TestData.AnonymouseFacade_CreateAirlineUser_Id,
            };
            return airlineCompany;
        }

        public User CreateAdminUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.AdminFacade_CreateAdminUser_Username,
                Password = TestData.AdminFacade_CreateAdminUser_Password,
                Email = TestData.AdminFacade_CreateAdminUser_Email,
                User_Role = TestData.AdminFacade_CreateAdminUser_UserRole
            };
            return airlineUser;
        }

        public Admin CreateAdminForTest()
        {
            userDAOPGSQL.Add(CreateAdminUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.AdminFacade_CreateAdminUser_Username);
            TestData.AdminFacade_CreateAdminUser_Id = (int)u.Id;

            Admin admin = new Admin
            {
                First_Name = TestData.AdminFacade_CreateAdmin_FirstName,
                Last_Name = TestData.AdminFacade_CreateAdmin_LastName,
                Level = TestData.AdminFacade_CreateAdmin_Level,
                User_id = TestData.AdminFacade_CreateAdminUser_Id
            };
            return admin;
        }

        public User CreateCustomerUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.AdminFacade_CreateCustomerUser_Username,
                Password = TestData.AdminFacade_CreateCustomerUser_Password,
                Email = TestData.AdminFacade_CreateCustomerUser_Email,
                User_Role = TestData.AdminFacade_CreateCustomerUser_UserRole
            };
            return airlineUser;
        }

        public Customer CreateCustomerForTest() {
            userDAOPGSQL.Add(CreateCustomerUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.AdminFacade_CreateCustomerUser_Username);
            TestData.AdminFacade_CreateCustomerUser_Id = (int)u.Id;

            Customer customer = new Customer()
            {
                First_Name = TestData.AdminFacade_CreateCustomer_FirstName,
                Last_Name = TestData.AdminFacade_CreateCustomer_LastName,
                Address = TestData.AdminFacade_CreateCustomer_Address,
                Phone_No = TestData.AdminFacade_CreateCustomer_PhoneNo,
                Credit_Card_No = TestData.AdminFacade_CreateCustomer_CreditCardNo,
                User_Id = TestData.AdminFacade_CreateCustomerUser_Id
            };
            return customer;

        }

        [TestMethod]
        public void CreateAdminTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            facadeAdmin.CreateAdmin(token, CreateAdminForTest());
            var list = adminDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);
            Assert.AreEqual(1, list.Count);
        }
        [TestMethod]
        public void CreateNewAirlineTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewAirline(token, CreateAirlineCompanyForTest());
            var list = airlineCompanyDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);

        }
        [TestMethod]
        public void CreateNewCustomerTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            
            var list = customerDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }
        [TestMethod]
        public void GetAllCustomersTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());

            var list = facadeAdmin.GetAllCustomers(token);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_FirstName, list[0].First_Name);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_LastName, list[0].Last_Name);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_Address, list[0].Address);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_PhoneNo, list[0].Phone_No);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_CreditCardNo, list[0].Credit_Card_No);
        }
        [TestMethod]
        public void RemoveAdminTest()
        {
            //DeleteAllData();
            //ILoginService loginService = new LoginService();
            //loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            //LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;


            //facadeAdmin.CreateAdmin(token, CreateAdminForTest());

            //var list = adminDAOPGSQL.GetAll();

            //facadeAdmin.RemoveAdmin(token, list[0]);

            //Assert.AreEqual(2, list.Count);

        }
        [TestMethod]
        public void RemoveAirlineTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewAirline(token, CreateAirlineCompanyForTest());
            var list = facadeAdmin.GetAllAirlineCompanies();
            facadeAdmin.RemoveAirline(token, list[0]);

            Assert.AreNotEqual(1, list.Count);
            Assert.AreEqual(0, list.Count);


        }

        [TestMethod]
        public void RemoveCustomerTest()
        {
            DeleteAllData();
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> token);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());
            var list = facadeAdmin.GetAllCustomers(token);

            list.RemoveAt(0);

            Assert.AreNotEqual(1, list.Count);
            Assert.AreEqual(0, list.Count);
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
