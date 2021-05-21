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
        CustomerDAOPGSQL customerDAOPGSQL = new CustomerDAOPGSQL();
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
        AdminDAOPGSQL adminDAOPGSQL = new AdminDAOPGSQL();

        public void getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin)
        {
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out token);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(token) as LoggedInAdministratorFacade;
        }

        public User CreateAirlineUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.CreateAirlineUser_Username,
                Password = TestData.CreateAirlineUser_Password,
                Email = TestData.CreateAirlineUser_Email,
                User_Role = TestData.CreateAirlineUser_UserRole
            };
            return airlineUser;
        }

        public AirlineCompany CreateAirlineCompanyForTest()
        {
            userDAOPGSQL.Add(CreateAirlineUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.CreateAirlineUser_Username);
            TestData.CreateAirlineUser_Id = (int)u.Id;

            AirlineCompany airlineCompany = new AirlineCompany
            {
                Name = TestData.CreateAirlineCompany_Name,
                Country_Id = TestData.CreateAirlineCompany_Country_Id,
                User_Id = TestData.CreateAirlineUser_Id,
            };
            return airlineCompany;
        }

        public User CreateAdminUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.CreateAdminUser_Username,
                Password = TestData.CreateAdminUser_Password,
                Email = TestData.CreateAdminUser_Email,
                User_Role = TestData.CreateAdminUser_UserRole
            };
            return airlineUser;
        }

        public Admin CreateAdminForTest()
        {
            userDAOPGSQL.Add(CreateAdminUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.CreateAdminUser_Username);
            TestData.CreateAdminUser_Id = (int)u.Id;

            Admin admin = new Admin
            {
                First_Name = TestData.CreateAdmin_FirstName,
                Last_Name = TestData.CreateAdmin_LastName,
                Level = TestData.CreateAdmin_Level,
                User_id = TestData.CreateAdminUser_Id
            };
            return admin;
        }
        public User CreateCustomerUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.CreateCustomerUser_Username,
                Password = TestData.CreateCustomerUser_Password,
                Email = TestData.CreateCustomerUser_Email,
                User_Role = TestData.CreateCustomerUser_UserRole
            };
            return airlineUser;
        }

        public Customer CreateCustomerForTest() {
            userDAOPGSQL.Add(CreateCustomerUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.CreateCustomerUser_Username);
            TestData.CreateCustomerUser_Id = (int)u.Id;

            Customer customer = new Customer()
            {
                First_Name = TestData.CreateCustomer_FirstName,
                Last_Name = TestData.CreateCustomer_LastName,
                Address = TestData.CreateCustomer_Address,
                Phone_No = TestData.CreateCustomer_PhoneNo,
                Credit_Card_No = TestData.CreateCustomer_CreditCardNo,
                User_Id = TestData.CreateCustomerUser_Id
            };
            return customer;
        }

        [TestMethod]
        public void CreateAdminTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateAdmin(token, CreateAdminForTest());
            var list = adminDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void CreateNewAirlineTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateNewAirline(token, CreateAirlineCompanyForTest());
            var list = airlineCompanyDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        public void CreateNewCustomerTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());
            var list = customerDAOPGSQL.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }
        [TestMethod]
        public void GetAllCustomersTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());

            var list = facadeAdmin.GetAllCustomers(token);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(TestData.CreateCustomer_FirstName, list[0].First_Name);
            Assert.AreEqual(TestData.CreateCustomer_LastName, list[0].Last_Name);
            Assert.AreEqual(TestData.CreateCustomer_Address, list[0].Address);
            Assert.AreEqual(TestData.CreateCustomer_PhoneNo, list[0].Phone_No);
            Assert.AreEqual(TestData.CreateCustomer_CreditCardNo, list[0].Credit_Card_No);
        }

        [TestMethod]
        public void RemoveAdminTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            adminDAOPGSQL.Add(CreateAdminForTest());
            var list = adminDAOPGSQL.GetAll();
            facadeAdmin.RemoveAdmin(token, list[0]);
            Admin a = adminDAOPGSQL.Get(list[0].Id);

            Assert.AreEqual(null, a);
        }

        [TestMethod]
        public void RemoveAirlineTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin); 
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;

            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());
            var list = airlineCompanyDAOPGSQL.GetAll();
            facadeAdmin.RemoveAirline(token, list[0]);
            list = facade.GetAllAirlineCompanies();

            Assert.AreNotEqual(1, list.Count);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void RemoveCustomerTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            customerDAOPGSQL.Add(CreateCustomerForTest());
            var list = customerDAOPGSQL.GetAll();

            facadeAdmin.RemoveCustomer(token, list[0]);
            list = facadeAdmin.GetAllCustomers(token);

            Assert.AreNotEqual(1, list.Count);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void UpdateAdminTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateAdmin(token, CreateAdminForTest());
            var list = adminDAOPGSQL.GetAll();
            Admin a = list[0];
            a.First_Name = TestData.UpdateAdmin_FirstName;
            a.Last_Name = TestData.UpdateAdmin_LastName;
            a.Level = TestData.UpdateAdmin_Level;

            facadeAdmin.UpdateAdmin(token, a);
            var a_new = adminDAOPGSQL.Get(a.Id);

            Assert.AreEqual(TestData.UpdateAdmin_FirstName, a_new.First_Name);
            Assert.AreEqual(TestData.UpdateAdmin_LastName, a_new.Last_Name);
            Assert.AreEqual(TestData.UpdateAdmin_Level, a_new.Level);
        }

        [TestMethod]
        public void UpdateAirlineDetailsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;

            facadeAdmin.CreateNewAirline(token, CreateAirlineCompanyForTest());
            var list = airlineCompanyDAOPGSQL.GetAll();
            AirlineCompany ac = list[0];

            ac.Name = TestData.UpdateAirlineCompany_Name;

            facadeAdmin.UpdateAirlineDetails(token, ac);
            var ac_new = airlineCompanyDAOPGSQL.Get((int)ac.Id);
            Assert.AreEqual(TestData.UpdateAirlineCompany_Name, ac_new.Name);

        }
        [TestMethod]
        public void UpdateCustomerDetailsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());
            var list = facadeAdmin.GetAllCustomers(token);
            Customer c = list[0];

            c.First_Name = TestData.UpdateCustomer_FirstName;
            c.Last_Name = TestData.UpdateCustomer_LastName;
            c.Address = TestData.UpdateCustomer_Address;
            c.Phone_No = TestData.UpdateCustomer_PhoneNo;
            c.Credit_Card_No = TestData.UpdateCustomer_CreditCardNo;

            facadeAdmin.UpdateCustomerDetails(token, c);
            var c_new = customerDAOPGSQL.Get((int)c.Id);

            Assert.AreEqual(TestData.UpdateCustomer_FirstName, c.First_Name);
            Assert.AreEqual(TestData.UpdateCustomer_LastName, c.Last_Name);
            Assert.AreEqual(TestData.UpdateCustomer_Address, c.Address);
            Assert.AreEqual(TestData.UpdateCustomer_PhoneNo, c.Phone_No);
            Assert.AreEqual(TestData.UpdateCustomer_CreditCardNo, c.Credit_Card_No);
        }
    }
}
