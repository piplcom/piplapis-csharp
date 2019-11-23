using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using Pipl.APIs;
using Pipl.APIs.Search;
using Pipl.APIs.Data;
using Pipl.APIs.Data.Fields;
using Pipl.APIs.Data.Containers;
using Pipl.APIs.Data.Enums;
using Pipl.APIs.Utils;


namespace UnitTestProjectAPIc
{
    [TestClass]
    public class UnitTest1
    {


        public UnitTest1()
        {
            //Init();
        }
        private SearchConfiguration setDefaultSearchConfiguration()
        {
            //edit your API key in appsettings  app.config file such as:   <appSettings > < add key = "PIPL_APIKey" value = "YOURKEY" /></ appSettings >
            //this is not working perhaps bec of project differences or because of test project;  see irregularTTextParser project
            //string api_key = ConfigurationManager.AppSettings.Get("Pipl_APIKey"); //?? throw new ArgumentNullException("No PIPL API key set in App.config");
            string api_key = "BUSINESS-PREMIUM-YOURKEY";

            return new SearchConfiguration()
            {
                ApiKey = api_key,
                ShowSources = ShowSources.Matching,
                //For improved latency, rate and consistent results
                LiveFeeds = false,

                //Work only with single matching profiles (Person responses), and must have social_profiles including LinkedIn sources
                // see https://pipl.com/api/reference/#match-criteria and https://pipl.com/api/reference/#source
                MinimumMatch = 1,
                MatchRequirements = "social_profiles",
                SourceCategoryRequirements = "professional_and_business",
                UseHttps = true

            };

        }

        [TestMethod]
        public void TestTopMatch()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            sConfig.TopMatch = true;
            SearchAPIRequest s = new SearchAPIRequest(firstName: "Moshe", lastName: "Elkayam", requestConfiguration: sConfig);
            SearchAPIResponse res = s.Send();

            Assert.IsTrue(res.TopMatch);
        }

        [TestMethod]
        public void TestNotTopMatch()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            SearchAPIRequest s = new SearchAPIRequest(firstName: "Moshe", lastName: "Elkayam", requestConfiguration: sConfig);
            SearchAPIResponse res = s.Send();

            Assert.IsFalse(res.TopMatch);
        }

        [TestMethod]
        public void aaa_testControlCheck()
        {

            Debug.WriteLine("hi. Control check. ");
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void TestPersonPhoneQParam()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            List<Field> fields = new List<Field>();
            var person = new Person(fields);

            SearchAPIRequest request = new SearchAPIRequest(phone: "+14152549431", requestConfiguration: sConfig);
            SearchAPIResponse response = request.Send();
            Assert.IsTrue(response.Person != null);

            request = new SearchAPIRequest(phone: "14152549431", requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.Person != null);

            request = new SearchAPIRequest(phone: "+14152549431", requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.Person != null);

            request = new SearchAPIRequest(phone: "+972508915495", requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.PersonsCount == 0);

            request = new SearchAPIRequest(phone: "4152549431", requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.Person != null);

            //fields.Add(new Phone(raw: "4152549431"));  //Clark Kent #
            //SearchAPIRequest request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            //fields.Add(new Phone(countryCode: System.Convert.ToInt32(recIn.CountryCode), number: System.Convert.ToInt64(recIn.PhoneNum)));

        }


        [TestMethod]
        public void TestPersonPhonePersonSearch()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            List<Field> fields = new List<Field>();

            fields.Add(new Phone(countryCode: 1, number: 4152549431));
            var person = new Person(fields);
            SearchAPIRequest request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            SearchAPIResponse response = request.Send();
            Assert.IsTrue(response.Person != null);

            fields = new List<Field>();
            fields.Add(new Phone(number: 4152549431));
            person = new Person(fields);
            request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.Person != null);

            fields = new List<Field>();
            fields.Add(new Phone(countryCode: 34, number: 636117049));
            person = new Person(fields);
            request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            response = request.Send();
            Assert.IsTrue(response.PersonsCount >= 0);
        }
        [TestMethod]
        public void TestPersonPhonePersonSearchNull()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            List<Field> fields = new List<Field>();

            fields = new List<Field>();
            fields.Add(new Phone(countryCode: null, number: 636117049));
            var person = new Person(fields);
            SearchAPIRequest request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            request = new SearchAPIRequest(person: person, requestConfiguration: sConfig);
            SearchAPIResponse response;

            try
            {
                response = request.Send();
                Assert.IsTrue(false); //if it gets this far, it's false;
            }
            catch (SearchAPIError apiError)
            {
                Assert.IsTrue(apiError.Error == "The query does not contain any valid name/username/user_id/phone/email/address to search by");
            }
            catch (System.Net.WebException e)
            { }
            catch (Exception e)
            { }

        }



        [TestMethod]
        public void TestIsPhoneSearchable()
        {
            SearchConfiguration sConfig = setDefaultSearchConfiguration();
            List<Field> fields = new List<Field>();

            fields.Add(new Phone(countryCode: 1, number: 4152549431));
            Assert.IsTrue(fields[0].IsSearchable);
            fields.Add(new Phone(countryCode: 9, number: 4152549431));
            Assert.IsTrue(fields[1].IsSearchable);
            fields.Add(new Phone(countryCode: 999, number: 4152549431));
            Assert.IsTrue(fields[2].IsSearchable);
            fields.Add(new Phone(countryCode: +1, number: +4152549431));
            Assert.IsTrue(fields[3].IsSearchable);
            fields.Add(new Phone(countryCode: 34, number: 636117049));
            Assert.IsTrue(fields[4].IsSearchable);
            fields.Add(new Phone(countryCode: +34, number: +636117049));
            Assert.IsTrue(fields[5].IsSearchable);
            fields.Add(new Phone(countryCode: -1, number: -4152549431));
            Assert.IsFalse(fields[6].IsSearchable);
            fields.Add(new Phone(countryCode: 9999, number: 636117049));
            Assert.IsFalse(fields[7].IsSearchable);
            fields.Add(new Phone(countryCode: 1, number: 99999));
            Assert.IsFalse(fields[8].IsSearchable);
            fields.Add(new Phone(countryCode: null, number: +4152549431));
            Assert.IsTrue(fields[9].IsSearchable);
            fields.Add(new Phone(countryCode: null, number: +4152549431));
            Assert.IsTrue(fields[10].IsSearchable);

        }

    }
}
