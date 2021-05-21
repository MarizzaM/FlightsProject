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
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_FirstName, list[0].First_Name);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_LastName, list[0].Last_Name);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_Address, list[0].Address);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_PhoneNo, list[0].Phone_No);
            Assert.AreEqual(TestData.AdminFacade_CreateCustomer_CreditCardNo, list[0].Credit_Card_No);
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
            a.First_Name = TestData.AdminFacade_UpdateAdmin_FirstName;
            a.Last_Name = TestData.AdminFacade_UpdateAdmin_LastName;
            a.Level = TestData.AdminFacade_UpdateAdmin_Level;

            facadeAdmin.UpdateAdmin(token, a);
            var a_new = adminDAOPGSQL.Get(a.Id);

            Assert.AreEqual(TestData.AdminFacade_UpdateAdmin_FirstName, a_new.First_Name);
            Assert.AreEqual(TestData.AdminFacade_UpdateAdmin_LastName, a_new.Last_Name);
            Assert.AreEqual(TestData.AdminFacade_UpdateAdmin_Level, a_new.Level);
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

            ac.Name = TestData.AnonymouseFacade_UpdateAirlineCompany_Name;

            facadeAdmin.UpdateAirlineDetails(token, ac);
            var ac_new = airlineCompanyDAOPGSQL.Get((int)ac.Id);
            Assert.AreEqual(TestData.AnonymouseFacade_UpdateAirlineCompany_Name, ac_new.Name);

        }
        [TestMethod]
        public void UpdateCustomerDetailsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> token, out LoggedInAdministratorFacade facadeAdmin);

            facadeAdmin.CreateNewCustomer(token, CreateCustomerForTest());
            var list = facadeAdmin.GetAllCustomers(token);
            Customer c = list[0];

            c.First_Name = TestData.AdminFacade_UpdateCustomer_FirstName;
            c.Last_Name = TestData.AdminFacade_UpdateCustomer_LastName;
            c.Address = TestData.AdminFacade_UpdateCustomer_Address;
            c.Phone_No = TestData.AdminFacade_UpdateCustomer_PhoneNo;
            c.Credit_Card_No = TestData.AdminFacade_UpdateCustomer_CreditCardNo;

            facadeAdmin.UpdateCustomerDetails(token, c);
            var c_new = customerDAOPGSQL.Get((int)c.Id);

            Assert.AreEqual(TestData.AdminFacade_UpdateCustomer_FirstName, c.First_Name);
            Assert.AreEqual(TestData.AdminFacade_UpdateCustomer_LastName, c.Last_Name);
            Assert.AreEqual(TestData.AdminFacade_UpdateCustomer_Address, c.Address);
            Assert.AreEqual(TestData.AdminFacade_UpdateCustomer_PhoneNo, c.Phone_No);
            Assert.AreEqual(TestData.AdminFacade_UpdateCustomer_CreditCardNo, c.Credit_Card_No);
        }
    }
}
