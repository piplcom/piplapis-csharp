using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


using Pipl.APIs.Search;
using Pipl.APIs.Data.Fields;
using Pipl.APIs.Data.Containers;

namespace UnitTests
{
   
    [TestClass]
    public class UnitTest1
    {
        private void SaveToFile(string fileName, string content){
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", fileName);
            destPath = Path.GetFullPath(destPath);
            File.WriteAllText(destPath, content);

            Console.WriteLine("write response to file " + destPath);
        }

        private async Task<string> SendRequest(string email, SearchAPIRequest request){
            HttpClient client = new HttpClient();

            string effectiveUrl = request.Url + String.Format("key={0}&email={1}", request.Configuration.ApiKey, email);

            var response2 = await client.GetAsync(effectiveUrl);
            HttpContent responseContent = response2.Content;
            var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
            return await reader.ReadToEndAsync();
        }

        private async Task TestRequest(string fileName, string originalFileName, string email){
            SearchAPIRequest request = new SearchAPIRequest(email: email);

            SearchAPIResponse response = request.Send();
            var originalJson = await SendRequest(email, request);
            var responseJson = JsonConvert.SerializeObject(response, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            SaveToFile(fileName, responseJson);
            SaveToFile(originalFileName, originalJson);



            Assert.IsNotNull(response.Person);
        }

        [TestMethod]
        public async Task TestSearch1()
        {
            await TestRequest("data1.json", "original_data1.json", "garth.moulton@pipl.com");
        }

        [TestMethod]
        public async Task TestSearch2()
        {
            await TestRequest("data2.json", "original_data2.json", "vrajajee@yahoo.com");
        }


        [TestMethod]
        public void TestSearchWithVin(){
            SearchAPIRequest request = new SearchAPIRequest(vin: "1GNDT13WXN2203169");

            SearchAPIResponse response = request.Send();

            Assert.IsNotNull(response.PossiblePersons);
            Assert.IsTrue(response.PossiblePersons.Any());

            Person person = response.PossiblePersons[0];

            Assert.IsTrue(person.Vehicles.Any());
        }

        [TestMethod]
        public void TestEmailType(){
            SearchAPIRequest request = new SearchAPIRequest(email: "garth.moulton@pipl.com");

            SearchAPIResponse response = request.Send();

            Assert.IsNotNull(response.Person);
            Assert.IsTrue(response.Person.Vehicles.Any());
            Assert.IsTrue(response.AvailableData.Premium.WorkEmails > 0);
            Assert.IsTrue(response.AvailableData.Premium.PersonalEmails > 0);
        }

        [TestMethod]
        public void TestVoipMatchRequirements(){
            SearchAPIRequest request = new SearchAPIRequest(email: "vrajajee@yahoo.com");
            request.Configuration.MatchRequirements = "phone.voip";

            SearchAPIResponse response = request.Send();

            Assert.IsNotNull(response.Person);
            Assert.IsTrue(response.PersonsCount > 0);
            Assert.IsTrue(response.AvailableData.Premium.VoipPhones > 0);
            Assert.IsTrue(response.Person.Phones.Any(phone => phone.Voip == true));

            request = new SearchAPIRequest(email: "garth.moulton@pipl.com");
            request.Configuration.MatchRequirements = "phone.voip";

            response = request.Send();

            Assert.IsTrue(response.PersonsCount == 0);
        }
    }
}
