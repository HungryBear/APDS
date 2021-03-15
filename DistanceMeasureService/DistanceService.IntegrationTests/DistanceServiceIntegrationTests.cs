using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DistanceService.IntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    public class DistanceServiceIntegrationTests
    {
        private const string serviceurl = "https://localhost:49157/distance/"; // TODO to appsettings / env. variables
        [TestMethod]
        public async Task ShouldRetrieveIataCodesAndEvalDistanceValueForTwoRandomCodes()
        {
            using (var client = new HttpClient())
            {
                var iataValuesEndpoint = $"{serviceurl}/iata";

                var iataCodesresponse = await client.GetAsync(iataValuesEndpoint);
                var values = JsonConvert.DeserializeObject(await iataCodesresponse.Content.ReadAsStringAsync()) as JArray;
                Assert.IsTrue(values?.Count > 0, "IATA Codes Query should return full list of code+coordinate pairs!.");

                var random = new Random();
                var srcIndex = random.Next(2, values.Count - 1);
                var dstIndex = random.Next(2, values.Count - 1);
                var distanceEvalUri = $"{serviceurl}/eval?src={values[srcIndex].Value<string>()}&dst={values[dstIndex].Value<string>()}";
                var distanceResponse = await client.GetAsync(distanceEvalUri);

                var distanceValue = await distanceResponse.Content.ReadAsStringAsync();
                Assert.IsTrue(double.TryParse(distanceValue, out var distance));
                Assert.IsTrue(distance > 0);
            }
        }
    }
}