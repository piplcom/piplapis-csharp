using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pipl.APIs.Search;

namespace UnitTests
{
   
    [TestClass]
    public class UnitTest1
    {
        public static string API_KEY = "YOUR API KEY HERE";

        [TestMethod]
        public void TestTopMatch()
        {
            SearchConfiguration config = new SearchConfiguration(topMatch: true, apiKey: API_KEY);
            SearchAPIRequest s = new SearchAPIRequest(firstName: "Moshe", lastName: "Elkayam", requestConfiguration: config);

            SearchAPIResponse res = s.Send();

            Assert.IsTrue(res.TopMatch);
        }

        [TestMethod]
        public void TestNotTopMatch()
        {
            SearchConfiguration config = new SearchConfiguration(apiKey: API_KEY);
            SearchAPIRequest s = new SearchAPIRequest(firstName: "Moshe", lastName: "Elkayam", requestConfiguration: config);

            SearchAPIResponse res = s.Send();

            Assert.IsFalse(res.TopMatch);
        }
    }
}
