using FlightsProject.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestWebApiFlightsProject
{
    [TestClass]
    public class UnitTest1
    {
        private readonly TestHostFixture _testHostFixture = new TestHostFixture();// Initializes the webHost
        private HttpClient _httpClient;//Http client used to send requests to the contoller

        [TestInitialize]
        public async Task SetUp()
        {
            _httpClient = _testHostFixture.Client;
        }

        [TestMethod]
        public async Task Search_Flight_With_No_Query_Parameters()
        {
            // add : POST PUT DELETE
            var response = await _httpClient.GetAsync("https://localhost:44323/api/Anonymous/get_all_airline_companies");

            var responseContent = await response.Content.ReadAsStringAsync();

            // different serialization
            List<Flight> flightListResult = JsonSerializer.Deserialize<List<Flight>>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.AreEqual(flightListResult.Count, 2);
        }

    }
}
