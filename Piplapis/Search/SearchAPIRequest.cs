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

namespace Pipl.APIs.Search
{
    /**
     * A request to Pipl's Search API.
     * <p/>
     * The request supports prioritizing/filtering the type of records you
     * prefer to get in the response (see the appendPriorityRule and
     * addRecordsFilter methods).
     * <p/>
     * Sending the request and getting the response is very simple and can be done
     * by either making a blocking call to request.Send() or by making a
     * non-blocking call to request.SendAsync(callback) which sends the request
     * asynchronously.
     */
    public class SearchAPIRequest
    {
        public string ApiKey { get; set; }
        public Person Person { get; set; }
        public string QueryParamsMode { get; set; }
        public bool ExactName { get; set; }
        private List<string> FilterRecordsBy { get; set; }
        private List<string> PrioritizeRecordsBy { get; set; }
        public static string BASE_URL = "http://api.pipl.com/search/v3/json/?";
        // HTTPS is also supported:
        //public static string BASE_URL = "https://api.pipl.com/search/v3/json/?";
        public static String defaultApiKey = null;

        /**
         * Initiate a new request object with given query params.
         * Each request must have at least one searchable parameter, meaning
         * a name (at least first and last name), email, phone or username.
         * Multiple query params are possible (for example querying by both email
         * and phone of the Person).
         *
         * @param ApiKey            A valid API key (use "samplekey" for experimenting).
         *                          Note that you can set a default API key
         *                          (Pipl.APIs.Search.SearchAPIRequest.defaultApiKey = '<your_key>') instead of
         *                          passing it to each request object.
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
         * @param rawAddress        An unparsed address
         * @param fromAge           fromAge
         * @param toAge             toAge
         * @param person            A Person object (Pipl.APIs.Data.Person).
         *                          The Person can contain every field allowed by the data-model
         *                          (see Pipl.APIs.Data.Fields) and can hold multiple fields of
         *                          the same type (for example: two emails, three addresses etc.)
         * @param queryParamsMode   One of "and"/"or" (default "and").
         *                          Advanced parameter, use only if you care about the
         *                          value of record.queryParamsMatch in the response
         *                          records.
         *                          Each record in the response has an attribute
         *                          "queryParamsMatch" which indicates whether the
         *                          record has the all fields from the query or not.
         *                          When set to "and" all query params are required in
         *                          order to get queryParamsMatch=true, when set to
         *                          "or" it's enough that the record has at least one
         *                          of each field type (so if you search with a name
         *                          and two addresses, a record with the name and one
         *                          of the addresses will have queryParamsMatch=true)
         * @param exactName         bool (default false).
         *                          If set to true the names in the query will be matched
         *                          "as is" without compensating for nicknames or multiple
         *                          family names. For example "Jane Brown-Smith" won't return
         *                          results for "Jane Brown" in the same way "Alexandra Pitt"
         *                          won't return results for "Alex Pitt".
         */
        public SearchAPIRequest(string apiKey = null, string firstName = null, string middleName = null,
                                string lastName = null, string rawName = null, string email = null, string phone = null,
                                string username = null, string country = null, string state = null, string city = null,
                                string rawAddress = null, int? fromAge = null, int? toAge = null, Person person = null,
                                string queryParamsMode = "and", bool exactName = false)
        {
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
                fields.Add(Phone.FromText(phone));
            }
            if (!String.IsNullOrEmpty(username))
            {
                fields.Add(new Username(username));
            }
            if (!String.IsNullOrEmpty(country) || !String.IsNullOrEmpty(state) || !String.IsNullOrEmpty(city))
            {
                fields.Add(new Address(country: country, state: state, city: city));
            }
            if (!String.IsNullOrEmpty(rawAddress))
            {
                fields.Add(new Address(raw: rawAddress));
            }
            if ((fromAge != null && fromAge >= 0) || (toAge != null && toAge >= 0))
            {
                // Need to cast back to int to remove their nullability
                fields.Add(DOB.FromAgeRange((int)fromAge, (int)toAge));
            }
            if (person == null)
            {
                person = new Person();
            }
            this.Person = person;
            Person.AddFields(fields);

            this.ApiKey = apiKey;
            this.QueryParamsMode = String.IsNullOrEmpty(queryParamsMode) ? "and" : queryParamsMode;
            this.ExactName = exactName;
            this.FilterRecordsBy = new List<string>();
            this.PrioritizeRecordsBy = new List<string>();
        }

        /**
         * Transform the params to the API format, return a list of params.
         *
         * @param domain
         * @param category
         * @param sponsored_source
         * @param hasField
         * @param hasFields
         * @param queryParamsMatch
         * @param queryPersonMatch
         * @return List of params
         * @throws Exception
         */
        private static List<string> prepareFilteringParams(string domain = null, string category = null,
                                                               bool? sponsoredSource = null, Type hasField = null,
                                                               List<Type> hasFields = null, bool? queryParamsMatch = null,
                                                               bool? queryPersonMatch = null)
        {
            if (queryParamsMatch != null && !(bool)queryParamsMatch)
            {
                throw new ArgumentException("queryParamsMatch can only be True");
            }
            if (queryPersonMatch != null && !(bool)queryPersonMatch)
            {
                throw new ArgumentException("queryPersonMatch can only be true");
            }

            List<string> parameters = new List<string>();
            if (domain != null)
            {
                parameters.Add("domain:" + domain);
            }
            if (category != null)
            {
                HashSet<string> set = new HashSet<string>();
                set.Add(category);
                Source.ValidateCategories(set);
                parameters.Add("category:" + category);
            }
            if (sponsoredSource != null) parameters.Add("sponsored_source:" + sponsoredSource.ToString().ToLower());
            if (queryParamsMatch != null) parameters.Add("query_params_match");
            if (queryPersonMatch != null) parameters.Add("query_person_match");
            if (hasFields == null)
            {
                hasFields = new List<Type>();
            }
            if (hasField != null)
            {
                hasFields.Add(hasField);
            }
            foreach (Type field in hasFields)
                if (field.IsSubclassOf(typeof(Data.Fields.Field)))
                    parameters.Add("has_field:" + field.Name);
            return parameters;
        }


        /**
         * Add a new "and" filter for the records returned in the response.
         * <p/>
         * IMPORTANT: This method can be called multiple times per request for
         * adding multiple "and" filters, each of these "and" filters is
         * interpreted as "or" with the other filters.
         * For example:
         * <p/>
         * Examples:
         * <p><blockquote><pre>
         * SearchAPIRequest apiRequest = new SearchAPIRequest(apiKey: "samplekey", username: "eric123");
         * List<Field> hasFields = new List<Field>();
         * apiRequest.addRecordsFilter(domain: "linkedin", hasFields: new List<Field>(){typeof(Phone)});
         * apiRequest.addRecordsFilter(hasFields: new List<Field>(){typeof(Phone), typeof(Job)});
         * <p/>
         * </pre></blockquote>
         * <p/>
         * The above request is only for records that are:
         * (from LinkedIn AND has a phone) OR (has a phone AND has a job).
         * Records that don't match this rule will not come back in the response.
         * <p/>
         * Please note that in case there are too many results for the query,
         * adding filters to the request can significantly improve the number of
         * useful results; when you define which records interest you, you'll
         * get records that would have otherwise be cut-off by the limit on the
         * number of records per query.
         *
         * @param domain             For example "linkedin.com", you may also use "linkedin"
         *                           but note that it'll match "linkedin.*" and "*.linkedin.*"
         *                          (any sub-domain and any TLD).
         * @param category           Any one of the categories defined in
         *                           Pipl.APIs.Data.source.Source.categories.
         * @param sponsoredSource    bool, true means you want just the records that
         *                           come from a sponsored source and false means you
         *                           don't want these records.
         * @param hasFields          A list of fields classes from Pipl.APIs.Data.Fields,
         *                           records must have content in all these fields.
         *                           For example: [Name, Phone] means you only want records
         *                           that has at least one name and at least one phone.
         * @param queryParamsMatch   true is the only possible value and it means you
         *                           want records that match all the params you passed
         *                           in the query.
         * @param queryPersonMatch   true is the only possible value and it means you
         *                           want records that are the same Person you
         *                           queried by (only records with
         *                           queryPersonMatch == 1.0, see the documentation
         *                           of record.queryPersonMatch for more details).
         * @throws Exception
         */
        public void addRecordsFilter(string domain = null, string category = null,
                bool? sponsoredSource = null, List<Type> hasFields = null,
                bool? queryParamsMatch = null, bool? queryPersonMatch = null)
        {
            List<string> parameters = prepareFilteringParams(domain, category, sponsoredSource,
                    null, hasFields, queryParamsMatch, queryPersonMatch);
            if (parameters != null)
            {
                FilterRecordsBy.Add(string.Join(" AND ", parameters));
            }
        }

        /**
         * Append a new priority rule for the records returned in the response.
         * <p/>
         * IMPORTANT: This method can be called multiple times per request for
         * adding multiple priority rules, each call can be with only one argument
         * and the order of the calls matter (the first rule added is the highest
         * priority, the second is second priority etc).
         * For example:
         * <p/>
         * Examples:
         * <p><blockquote><pre>
         * SearchAPIRequest apiRequest = new SearchAPIRequest(apiKey: "samplekey", username: "eric123");
         * apiRequest.appendPriorityRule(domain: "linkedin");
         * apiRequest.appendPriorityRule(hasField: typeof(Phone));
         * <p/>
         * </pre></blockquote>
         * <p/>
         * In the response to the above request records from LinkedIn will be
         * returned before records that aren't from LinkedIn and records with
         * phone will be returned before records without phone.
         * <p/>
         * Please note that in case there are too many results for the query,
         * adding priority rules to the request does not only affect the order
         * of the records but can significantly improve the number of useful
         * results; when you define which records interest you, you'll get records
         * that would have otherwise be cut-off by the limit on the number
         * of records per query.
         *
         * @param domain             For example "linkedin.com", "linkedin" is also possible
         *                           and it'll match "linkedin.*".
         * @param category           Any one of the categories defined in
         *                           Pipl.APIs.Data.source.Source.categories.
         * @param sponsoredSource    true will bring the records that
         *                           come from a sponsored source first and false
         *                           will bring the non-sponsored records first.
         * @param hasField           A field type from Pipl.APIs.Data.Fields.
         *                           For example: hasField=typeof(Phone) means you want to give
         *                           a priority to records that has at least one phone.
         * @param queryParamsMatch   true is the only possible value and it means you
         *                           want to give a priority to records that match all
         *                           the params you passed in the query.
         * @param queryPersonMatch   true is the only possible value and it means you
         *                           want to give a priority to records with higher
         *                           queryPersonMatch (see the documentation of
         *                           record.queryPersonMatch for more details)
         * @throws ArgumentException ArgumentException is raised in any case of an invalid parameter.
         */
        public void appendPriorityRule(string domain = null, string category = null,
                                       bool? sponsoredSource = null, Type hasField = null,
                                       bool? queryParamsMatch = null, bool? queryPersonMatch = null)
        {
            List<string> parameters = SearchAPIRequest.prepareFilteringParams(domain, category, sponsoredSource,
                    hasField, null, queryParamsMatch, queryPersonMatch);
            if (parameters.Count > 1)
            {
                throw new ArgumentException("The function should be called with one argument");
            }
            if (parameters != null)
            {
                PrioritizeRecordsBy.Add(parameters[0]);
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
        public void validateQueryParams(bool strict = true)
        {
            if (String.IsNullOrEmpty(this.ApiKey) && String.IsNullOrEmpty(SearchAPIRequest.defaultApiKey))
            {
                throw new ArgumentException("API key is missing");
            }
            if (strict && (!"and".Equals(QueryParamsMode) && !"or".Equals(QueryParamsMode)))
            {
                throw new ArgumentException("query_params_match should be one of and/or");
            }
            if (!Person.IsSearchable)
            {
                throw new ArgumentException("No valid name/username/phone/email in request");
            }
            if (strict && Person.UnsearchableFields.Count > 0)
            {
                throw new ArgumentException("Some fields are unsearchable: " + Person.UnsearchableFields);
            }
        }

        /**
         * The URL of the request (as a string).
         *
         * @return encoded url
         * @throws IOException
         */
        [JsonIgnore]
        public string Url
        {
            get
            {
                return String.Format("{0}key={1}&person={2}&query_params_mode={3}&exact_name={4}&prioritize_records_by={5}&filter_records_by={6}",
                    BASE_URL,
                    Uri.EscapeDataString((String.IsNullOrEmpty(ApiKey) ? SearchAPIRequest.defaultApiKey : ApiKey)),
                    Uri.EscapeDataString(JsonConvert.SerializeObject(Person, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })),
                    Uri.EscapeDataString(QueryParamsMode),
                    Uri.EscapeDataString(ExactName.ToString()),
                    Uri.EscapeDataString(String.Join(",", PrioritizeRecordsBy)),
                    Uri.EscapeDataString(String.Join(",", FilterRecordsBy)));
            }
        }

        /**
         * Send the request and return the response or raise SearchAPIError.
         * <p/>
         * Calling this method blocks the program until the response is returned,
         * if you want the request to be sent asynchronously please refer to the
         * SendAsync method.
         *
         * @param strictValidation  A bool argument that's passed to the
         *                          validateQueryParams method.
         * @return The response is returned as a SearchAPIResponse object.
         * @throws ArgumentException Raises ArgumentException (raised from validateQueryParams)
         * @throws IOException              IOException
         * @throws SearchAPIError           SearchAPIError (when the response is returned but contains an error).
         */
        public SearchAPIResponse Send(bool strictValidation = true)
        {
            validateQueryParams(strictValidation);
            using (WebClient client = new WebClient())
            {
                WebException contextException = null;
                HttpWebResponse response;
                try
                {
                    string responseBody = client.DownloadString(Url);
                    return JsonConvert.DeserializeObject<SearchAPIResponse>(responseBody);
                }
                catch (WebException we)
                {
                    contextException = we;
                    response = (HttpWebResponse) we.Response;
                    if (response == null) throw;
                }
                catch (Exception e)
                {
                    
                    throw;
                }
                Dictionary<string, object> err;
                try
                {
                    string result;
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        result = sr.ReadToEnd();
                    err = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                }
                catch
                {
                    throw contextException;
                }
                string error = null;
                if (!err.ContainsKey("error"))
                    throw contextException;

                error = err["error"].ToString();
                int httpStatusCode;
                if (!err.ContainsKey("@http_status_code"))
                    throw contextException;

                if (!int.TryParse(err["@http_status_code"].ToString(), out httpStatusCode))
                    throw contextException;

                throw new SearchAPIError(error, httpStatusCode);
            }
        }

        /**
         * Same as Send() but in a non-blocking way.
         * <p/>
         * Use this method if you want to send the request asynchronously so your
         * program can do other things while waiting for the response.
         *
         * @param searchAPICallBack     a callback that will receive the response once the response is
	     *                              returned
         * @param strictValidation      A bool argument that's passed to the
         *                              validateQueryParams method.
         * @throws ArgumentException    Raises ArgumentException (raised from validateQueryParams)
         * @throws IOException          IOException
         */
        public void SendAsync(SearchAPICallBack searchAPICallBack, bool strictValidation = true)
        {
            validateQueryParams(strictValidation);
            using (WebClient client = new WebClient())
            {
                Uri uri = new Uri(Url);
                client.DownloadStringCompleted +=
                    new DownloadStringCompletedEventHandler(SearchDownloadStringCompletedEventHandler);

                client.DownloadStringAsync(uri, searchAPICallBack);
            }
        }

        public void SearchDownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            var searchApiCallBack = (SearchAPICallBack)e.UserState;
            if (e.Error == null)
            {
                searchApiCallBack.callback(JsonConvert.DeserializeObject<SearchAPIResponse>(e.Result));
                return;
            }
            if (!(e.Error is WebException))
            {
                searchApiCallBack.errback(e.Error);
                return;
            }
            
            WebException we = (WebException)e.Error;
            HttpWebResponse response = (HttpWebResponse)we.Response;
            if (response == null)
            {
                searchApiCallBack.errback(e.Error);
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
                searchApiCallBack.errback(ex);
                return;
            }

            Dictionary<string, object> err = null;
                
            try
            {
                err = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
            }
            catch (Exception ex)
            {
                searchApiCallBack.errback(ex);
                return;
            }

            if (!err.ContainsKey("error"))
            {
                searchApiCallBack.errback(we);
                return;
            }
            string error = err["error"].ToString();
            if (!err.ContainsKey("@http_status_code"))
            {
                searchApiCallBack.errback(we);
                return;
            }
            int httpStatusCode;
            if (!int.TryParse(err["@http_status_code"].ToString(), out httpStatusCode))
            {
                searchApiCallBack.errback(we);
                return;
            }
            searchApiCallBack.errback(new SearchAPIError(error, httpStatusCode));
            return;
        }
    }
}
