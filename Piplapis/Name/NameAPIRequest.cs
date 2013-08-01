using System;
using System.Net;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;
using System.IO;
using System.Collections.Generic;

namespace Pipl.APIs.Name
{
    /**
     * A request to Pipl's Name API.
     * <p/>
     * A request is build with a name that can be provided parsed to
     * first/Middle/last (in case it's already available to you parsed)
     * or unparsed (and then the API will parse it).
     * <p/>
     * Note that the name in the request can also be just a first-name or just
     * a last-name.
     */
    public class NameAPIRequest
    {
        public static string BASE_URL = "http://api.pipl.com/name/v2/json/?";
        // HTTPS is also supported:
        //public static string BASE_URL = "https://api.pipl.com/name/v2/json/?";
        public static string defaultApiKey = null;

        public string ApiKey { get; set; }

        public Data.Fields.Name Name { get; private set; }

        /**
         * Examples:
         * <p><blockquote><pre>
         * NameAPIRequest.defaultApiKey = "samplekey";
         * NameAPIRequest request1 = new NameAPIRequest(firstName: "Eric", lastName: "Cartman");
         * NameAPIRequest request2 = new NameAPIRequest(lastName: "Cartman");
         * NameAPIRequest request3 = new NameAPIRequest(rawName: "Eric Cartman");
         * NameAPIRequest request4 = new NameAPIRequest(rawName: "Eric");
         * <p/>
         * </pre></blockquote>
         *
         * @param apiKey     A valid API key, use "samplekey" for
         *                   experimenting, note that you can set a default API key
         *                   (NameAPIRequest.defaultApiKey = "<your_key>") instead of passing it
         *                   to each request object.
         * @param firstName  firstName
         * @param middleName middleName
         * @param lastName   lastName
         * @param rawName    rawName
         */
        public NameAPIRequest(string apiKey = null, string firstName = null, string middleName = null, 
            string lastName = null, string rawName = null)
        {
            if (String.IsNullOrEmpty(apiKey) && String.IsNullOrEmpty(NameAPIRequest.defaultApiKey))
            {
                throw new ArgumentException("A valid API key is required");
            }
            if (!(!String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName) 
			      || !String.IsNullOrEmpty(middleName) || !String.IsNullOrEmpty(rawName))) {
                throw new ArgumentException("A name is missing");
            }
            if (!String.IsNullOrEmpty(rawName) 
			    && (!String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(middleName) || !String.IsNullOrEmpty(lastName))) {
                throw new ArgumentException("Name should be provided raw or parsed, not both");
            }
            this.ApiKey = String.IsNullOrEmpty(apiKey) ? NameAPIRequest.defaultApiKey : apiKey;
            this.Name = new Data.Fields.Name(first: firstName, middle: middleName, last: lastName, raw: rawName);
        }

        /**
         * @return The URL of the request (as a string).
         */
        [JsonIgnore]
        public string Url {
            get
            {
                return String.Format("{0}&key={1}&first_name={2}&middle_name={3}&last_name={4}&raw_name={5}",
                    BASE_URL,
                    Uri.EscapeDataString(ApiKey),
                    Uri.EscapeDataString(this.Name.First ?? string.Empty),
                    Uri.EscapeDataString(this.Name.Middle ?? string.Empty),
                    Uri.EscapeDataString(this.Name.Last ?? string.Empty),
                    Uri.EscapeDataString(this.Name.Raw ?? string.Empty));
            }
        }


        /**
         * Send the request and return the response or raise NameAPIError.
         *
         * @return <code>NameAPIResponse</code> object
         * @throws NameAPIError
         * @throws WebException
         */
        public NameAPIResponse send() 
        {
            using(WebClient client = new WebClient()) {
                try
                {
                    string responseBody = client.DownloadString(Url);
                    return JsonConvert.DeserializeObject<NameAPIResponse>(responseBody);
                }
                catch (Exception e)
                {
                    if (e.GetType().Name == "WebException")
                    {
                        WebException we = (WebException)e;
                        HttpWebResponse response = (System.Net.HttpWebResponse)we.Response;
                        if (response == null) throw;
                        string result;
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            result = sr.ReadToEnd();
                        }
                        Dictionary<string, object> err = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                        throw new NameAPIError(err["error"].ToString(), Convert.ToInt32(err["@http_status_code"].ToString()));
                    }
                    throw;
                }
            }
        }
    }
}
