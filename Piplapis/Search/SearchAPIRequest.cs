using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Pipl.APIs.Data;
using Pipl.APIs.Data.Containers;
using Pipl.APIs.Data.Fields;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Linq;
using Pipl.APIs.Utils;
using System.Threading.Tasks;
using System.Globalization;

namespace Pipl.APIs.Search
{
    /**
     * A request to Pipl's Search API.
     * <p/>
     * Sending the request and getting the response is very simple and can be done
     * by either making a blocking call to request.Send() or by making a
     * non-blocking call to request.SendAsync(callback) which sends the request
     * asynchronously.
     */
    public class SearchAPIRequest
    {
        #region Static

        public static SearchConfiguration DefaultConfiguration;

        // HTTP URL
        private static string BaseUrlHttp = "http://api.pipl.com/search/?";
        // HTTPS is also supported:
        private static string BaseUrlHttpS = "https://api.pipl.com/search/?";

        // static CTOR
        static SearchAPIRequest()
        {
            DefaultConfiguration = new SearchConfiguration();
        }

        #endregion

        
        public Person Person { get; set; }

        public SearchConfiguration Configuration;

        /**
         * The URL of the request (as a string).
         *
         * @return encoded url
         * @throws IOException
         */
        [JsonIgnore]
        public string Url { get; private set; }

        public SearchConfiguration EffectiveConfiguration
        {
            get
            {
                if (Configuration != null)
                    return Configuration;

                return DefaultConfiguration;
            }
        }

        /**
         * The parameters of the request (as a NameValueCollection).
         *
         * @return Collection of the request's parameters
         * @throws IOException
         */
        private NameValueCollection _getUrlParams()
        {
            var res = new NameValueCollection();

            res.Add("key", (String.IsNullOrEmpty(EffectiveConfiguration.ApiKey) ? SearchConfiguration.DefaultApiKey : EffectiveConfiguration.ApiKey));
            res.Add("person", JsonConvert.SerializeObject(Person, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            if (EffectiveConfiguration.MinimumProbability != null)
                res.Add("minimum_probability", ((float)EffectiveConfiguration.MinimumProbability).ToString(CultureInfo.CreateSpecificCulture("en-US")));
            if (EffectiveConfiguration.ShowSources.HasValue)
                res.Add("show_sources", EnumExtensions.JsonEnumName(EffectiveConfiguration.ShowSources.Value));
            if (EffectiveConfiguration.HideSponsored != null)
                res.Add("hide_sponsored", EffectiveConfiguration.HideSponsored.ToString());
            if (EffectiveConfiguration.LiveFeeds != null)
                res.Add("live_feeds", EffectiveConfiguration.LiveFeeds.ToString());
            if (!String.IsNullOrEmpty(Person.SearchPointer))
                res.Add("search_pointer", Person.SearchPointer);
            if (EffectiveConfiguration.MinimumMatch != null){
                res.Add("minimum_match", ((float) EffectiveConfiguration.MinimumMatch).ToString(CultureInfo.CreateSpecificCulture("en-US")));
            }
            if (EffectiveConfiguration.MatchRequirements != null)
            {
                res.Add("match_requirements", EffectiveConfiguration.MatchRequirements);
            }
            if (EffectiveConfiguration.SourceCategoryRequirements != null)
            {
                res.Add("source_category_requirements", EffectiveConfiguration.SourceCategoryRequirements);
            }
                

            return res;
        }

        /**
         * Initiate a new request object with given query params.
         * Each request must have at least one searchable parameter, meaning
         * a name (at least first and last name), email, phone or username.
         * Multiple query params are possible (for example querying by both email
         * and phone of the Person).
         *
         * @param firstName         First name, minimum 2 chars
         * @param middleName        Middle name
         * @param lastName          Last name, minimum 2 chars
         * @param rawName           An unparsed name containing at least a first name
         *                          and a last name.
         * @param email             email
         * @param phone             a String that will be striped from all non-digit characters
         *                          and converted to Long. IMPORTANT: Currently only US/Canada
         *                          phones can be searched by so country code is assumed to be 1,
         *                          phones with different country codes are considered invalid and
         *                          will be ignored.
         *                          IMPORTANT: Currently only US/Canada phones can be searched by
         *                          so country code is assumed to be 1, phones with different
         *                          country codes are considered invalid and will be ignored.
         * @param username          username, minimum 4 chars
         * @param country           a 2 letter country code from:
         *                          http://en.wikipedia.org/wiki/ISO_3166-2
         * @param state             a state code from:
         *                          http://en.wikipedia.org/wiki/ISO_3166-2%3AUS
         *                          http://en.wikipedia.org/wiki/ISO_3166-2%3ACA
         * @param city              city
         * @param zip_code          zipCode
         * @param rawAddress        An unparsed address
         * @param fromAge           fromAge
         * @param toAge             toAge
         * @param person            A Person object (Pipl.APIs.Data.Person).
         *                          The Person can contain every field allowed by the data-model
         *                          (see Pipl.APIs.Data.Fields) and can hold multiple fields of
         *                          the same type (for example: two emails, three addresses etc.)
         * @param searchPointer     A search pointer (from a Possible Person object), to be used for drill-down searches.                         
         * @param requestConfiguration      RequestConfiguration object. If null, the default RequestConfiguration object is used               
         */
        public SearchAPIRequest(string firstName = null, string middleName = null,
                                string lastName = null, string rawName = null, string email = null, string phone = null,
                                string username = null, string country = null, string state = null, string city = null, string zipCode = null,
                                string rawAddress = null, int? fromAge = null, int? toAge = null, Person person = null, 
                                string searchPointer = null, SearchConfiguration requestConfiguration = null)
        {
            Configuration = requestConfiguration;

            List<Field> fields = new List<Field>();


            if (!String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(middleName) || !String.IsNullOrEmpty(lastName))
            {
                fields.Add(new Data.Fields.Name(first: firstName, middle: middleName, last: lastName));
            }
            if (!String.IsNullOrEmpty(rawName))
            {
                fields.Add(new Data.Fields.Name(raw: rawName));
            }
            if (!String.IsNullOrEmpty(email))
            {
                fields.Add(new Email(address: email));
            }
            if (!String.IsNullOrEmpty(phone))
            {
                fields.Add(new Phone(raw: phone));
            }
            if (!String.IsNullOrEmpty(username))
            {
                fields.Add(new Username(username));
            }
            if (!String.IsNullOrEmpty(country) || !String.IsNullOrEmpty(state) || !String.IsNullOrEmpty(city) || !String.IsNullOrEmpty(zipCode))
            {
                fields.Add(new Address(country: country, state: state, city: city, zip_code: zipCode));
            }
            if (!String.IsNullOrEmpty(rawAddress))
            {
                fields.Add(new Address(raw: rawAddress));
            }
            if ((fromAge != null) || (toAge != null))
            {
                fields.Add(DOB.FromAgeRange((fromAge == null) ? 0 : (int)fromAge, 
                                            (toAge == null) ? 1000 : (int)toAge));
            }
            if (person == null)
            {
                person = new Person();
            }
            if (searchPointer != null)
            {
                person.SearchPointer = searchPointer;
            }
            this.Person = person;
            Person.AddFields(fields);

            if (String.IsNullOrEmpty(EffectiveConfiguration.Url))
            {
                Url = (EffectiveConfiguration.UseHttps) ? BaseUrlHttpS : BaseUrlHttp;
            }
            else
            {
                Url = EffectiveConfiguration.Url;
            }
        }


        /**
         * Check if the request is valid and can be sent, raise ArgumentException if
         * not.
         *
         * @param strict A bool argument that defaults to true which means an
         *               exception is raised on every invalid query parameter, if set to false
         *               an exception is raised only when the search request cannot be performed
         *               because required query params are missing.
         */
        public void ValidateQueryParams(bool strict = true)
        {
            if (String.IsNullOrEmpty(EffectiveConfiguration.ApiKey) && String.IsNullOrEmpty(SearchConfiguration.DefaultApiKey))
            {
                throw new ArgumentException("API key is missing");
            }
            if ((EffectiveConfiguration.MinimumProbability != null) && (EffectiveConfiguration.MinimumProbability < 0 || EffectiveConfiguration.MinimumProbability > 1))
            {
                throw new ArgumentException("minimum_probability must have a value between 0 and 1");
            }
            if (!Person.IsSearchable)
            {
                throw new ArgumentException("No valid name/username/userid/phone/email/url in request");
            }
            if (strict && Person.UnsearchableFields.Count() > 0)
            {
                var unsearchableFields = from field in Person.UnsearchableFields select field.Repr();
                throw new ArgumentException("Some fields are unsearchable: " + String.Join(", ", unsearchableFields));
            }
            if ((EffectiveConfiguration.MinimumMatch != null) && (EffectiveConfiguration.MinimumMatch < 0 || EffectiveConfiguration.MinimumMatch > 1))
            {
                throw new ArgumentException("minimum_match must have a value between 0 and 1");
            }
        }

        /**
         * Send the request and return the response or raise SearchAPIError.
         * <p/>
         *
         * @param strictValidation      A bool argument that's passed to the
         *                              validateQueryParams method.
         * @return SearchAPIResponse object containing the response
         * @throws ArgumentException    Raises ArgumentException (raised from validateQueryParams)
         * @throws WebException         WebException
         * @throws SearchAPIError       SearchAPIError with api specific error. 
         */
        public SearchAPIResponse Send(bool strictValidation = true)
        {
            ValidateQueryParams(strictValidation);
            TaskCompletionSource<SearchAPIResponse> taskCompletionSource = new TaskCompletionSource<SearchAPIResponse>();

            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(Url);
                try
                {
                    byte[] response = client.UploadValues(Url, _getUrlParams());
                    string jsonResp = System.Text.Encoding.UTF8.GetString(response);
                    var res = JsonConvert.DeserializeObject<SearchAPIResponse>(System.Text.Encoding.UTF8.GetString(response));
                    res.RawJSON = jsonResp;
                    _update_response_headers(client, res);
                    return res;
                }
                catch (WebException e)
                {
                    if (e.Response == null)
                    {
                        throw e;
                    }
                    string result;
                    using (StreamReader sr = new StreamReader(e.Response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                    Dictionary<string, object> err = null;
                    err = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);

                    if (!err.ContainsKey("error"))
                    {
                        throw e;
                    }
                    string error = err["error"].ToString();
                    if (!err.ContainsKey("@http_status_code"))
                    {
                        throw e;
                    }
                    int httpStatusCode;
                    if (!int.TryParse(err["@http_status_code"].ToString(), out httpStatusCode))
                    {
                        throw e;
                    }
                    throw new SearchAPIError(error, httpStatusCode);
                }
            }
        }

        /**
         * Send the request and return the response or raise SearchAPIError.
         * <p/>
         *
         * @param strictValidation      A bool argument that's passed to the
         *                              validateQueryParams method.
         * @return A task that runs the request asyncronously
         * @throws ArgumentException    Raises ArgumentException (raised from validateQueryParams)
         * @throws IOException          IOException
         */
        public Task<SearchAPIResponse> SendAsync(bool strictValidation = true)
        {
            ValidateQueryParams(strictValidation);
            TaskCompletionSource<SearchAPIResponse> taskCompletionSource = new TaskCompletionSource<SearchAPIResponse>();

            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(Url);
                client.UploadValuesCompleted += (s, e) =>
                {
                    _searchUploadValuesCompletedEventHandler((WebClient)s, e, taskCompletionSource);
                };
                client.UploadValuesAsync(uri, null, _getUrlParams(), null);
            }

            return taskCompletionSource.Task;
        }

        private void _searchUploadValuesCompletedEventHandler(WebClient client, UploadValuesCompletedEventArgs e, TaskCompletionSource<SearchAPIResponse> taskCompletionSource)
        {
            if (e.Error == null)
            {
                string jsonResp = System.Text.Encoding.UTF8.GetString(e.Result);
                var res = JsonConvert.DeserializeObject<SearchAPIResponse>(jsonResp);
                res.RawJSON = jsonResp;
                _update_response_headers(client, res);
                taskCompletionSource.SetResult(res);
                return;
            }
            if (!(e.Error is WebException))
            {
                taskCompletionSource.SetException(e.Error);
                return;
            }
            
            WebException we = (WebException)e.Error;
            HttpWebResponse response = (HttpWebResponse)we.Response;
            if (response == null)
            {
                taskCompletionSource.SetException(e.Error);
                return;
            }
            string result;
            try
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
                return;
            }

            Dictionary<string, object> err = null;
                
            try
            {
                err = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
                return;
            }

            if (!err.ContainsKey("error"))
            {
                taskCompletionSource.SetException(we);
                return;
            }
            string error = err["error"].ToString();
            if (!err.ContainsKey("@http_status_code"))
            {
                taskCompletionSource.SetException(we);
                return;
            }
            int httpStatusCode;
            if (!int.TryParse(err["@http_status_code"].ToString(), out httpStatusCode))
            {
                taskCompletionSource.SetException(we);
                return;
            }
            taskCompletionSource.SetException(new SearchAPIError(error, httpStatusCode));
            return;
        }

        private void _update_response_headers(WebClient client, SearchAPIResponse resp)
        {
            WebHeaderCollection headers = client.ResponseHeaders;


            string value = headers.Get("X-APIKey-QPS-Allotted");
            int intVal;
            if (value != null) 
            {
                if (Int32.TryParse(value, out intVal))
                {
                    resp.QpsAllotted = intVal;
                }
            }

            value = headers.Get("X-APIKey-QPS-Current");
            if (value != null)
            {
                if (Int32.TryParse(value, out intVal))
                {
                    resp.QpsCurrent = intVal;
                }
            }

            value = headers.Get("X-APIKey-Quota-Allotted");
            if (value != null)
            {
                if (Int32.TryParse(value, out intVal))
                {
                    resp.QuotaAllotted = intVal;
                }
            }

            value = headers.Get("X-APIKey-Quota-Current");
            if (value != null)
            {
                if (Int32.TryParse(value, out intVal))
                {
                    resp.QuotaCurrent = intVal;
                }
            }

            value = headers.Get("X-Quota-Reset");
            if (value != null)
            {
                try
                {
                    value = value.Replace(" UTC", " +0");
                    resp.QuotaReset = DateTime.ParseExact(value, "dddd, MMMM dd, yyyy hh:mm:ss tt z", CultureInfo.InvariantCulture);
                }
                catch {}
            }

        }
    }
}
