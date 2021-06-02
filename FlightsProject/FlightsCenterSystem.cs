using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.I_DAO;
using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightsProject
{
    public class FlightsCenterSystem
    {
        private static FlightsCenterSystem Instance;
        private static object key = new object();
        private FlightDAOPGSQL oldFlights = new FlightDAOPGSQL();
        private TicketDAOPGSQL oldTickets = new TicketDAOPGSQL();
        public FlightsCenterSystem()
        {
            Task.Run(() => {
                while (true)
                {
                    //oldTickets.transferToTicketHistory();
                    //oldFlights.transferToFlighsHistory();

                    Thread.Sleep(86_400_000); //24 hours
                }
            });
        }

        public static FlightsCenterSystem GetInstance()
        {
            if (Instance == null)
            {
                lock (key)
                {
                    if (Instance == null)
                    {
                        Instance = new FlightsCenterSystem();
                    }
                }
            }
            return Instance;
        }

        //public FacadeBase GetFacade<T>(LoginToken<T> token) where T : IUser
        //{
        //    if (token.GetType() == typeof(Admin))
        //    {
        //        return new LoggedInAdministratorFacade();
        //    }
        //    if (token.GetType() == typeof(AirlineCompany))
        //    {
        //        return new LoggedsInAirlineFacade();
        //    }
        //    if (token.GetType() == typeof(Customer))
        //    {
        //        return new LoggedInCustomerFacade();
        //    }

        //    return new AnonymousUserFacade();
        //}

        public FacadeBase GetFacade<T>(LoginToken<T> token) where T : IUser
        {
            if (typeof(T) == typeof(Customer))
                return new LoggedInCustomerFacade();
            if (typeof(T) == typeof(AirlineCompany))
                return new LoggedsInAirlineFacade();
            if (typeof(T) == typeof(Admin))
                return new LoggedInAdministratorFacade();

            return new AnonymousUserFacade();
        }

        public ILoginToken Login(string userName, string Password)
        {
            LoginService loginService = new LoginService();
            ILoginToken token;

            if (loginService.TryAdminLogin(userName, Password, out LoginToken<Admin> AdminToken))
            {
                token = AdminToken;
            }
            else if (loginService.TryAirlineLogin(userName, Password, out LoginToken<AirlineCompany> AirlineCompanyToken))
            {
                token = AirlineCompanyToken;
            }
            else if (loginService.TryCustomerLogin(userName, Password, out LoginToken<Customer> CustomerToken))
            {
                token = CustomerToken;
            }
            else
                token = null;

            return token;
        }
    }
}
