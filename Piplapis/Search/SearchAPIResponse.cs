using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;
using Pipl.APIs.Data.Containers;

namespace Pipl.APIs.Search
{
    /**
     * A response from Pipl's Search API.
     * <p/>
     * A response comprises the two things returned as a result to your query:
     * <p/>
     * - A person (Pipl.APIs.Data.Person) that is the deta object
     * representing all the information available for the person you were
     * looking for.
     * This object will only be returned when our identity-resolution engine is
     * convinced that the information is of the person represented by your query.
     * Obviously, if the query was for "John Smith" there's no way for our
     * identity-resolution engine to know which of the hundreds of thousands of
     * people named John Smith you were referring to, therefore you can expect
     * that the response will not contain a person object.
     * On the other hand, if you search by a unique identifier such as email or
     * a combination of identifiers that only lead to one person, such as
     * "Eric Cartman, Age 22, From South Park, CO, US", you can expect to get
     * a response containing a single person object.
     * <p/>
     * - A list of records (Pipl.APIs.Data.Containers.Record) that fully/partially
     * match the person from your query, if the query was for "Eric Cartman from
     * Colorado US" the response might also contain records of "Eric Cartman
     * from US" (without Colorado), if you need to differentiate between records
     * with full match to the query and partial match or if you want to get a
     * score on how likely is that record to be related to the person you are
     * searching please refer to the record's attributes
     * record.queryParamsMatch and record.queryPersonMatch.
     * <p/>
     * The response also contains the query as it was interpreted by Pipl. This
     * part is useful for verification and debugging, if some query parameters
     * were invalid you can see in response.query that they were ignored, you can
     * also see how the name/address from your query were parsed in case you
     * passed raw_name/raw_address in the query.
     * <p/>
     * In some cases when the query isn't focused enough and can't be matched to
     * a specific person, such as "John Smith from US", the response also contains
     * a list of suggested searches. This is a list of Record objects, each of
     * these is an expansion of the original query, giving additional query
     * parameters so the you can zoom in on the right person.
     */
    public class SearchAPIResponse
    {
        [JsonProperty("query")]
        public Person Query { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("records")]
        public List<Record> Records { get; set; }

        [JsonProperty("suggested_searches")]
        public List<Record> SuggestedSearches { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        /**
         * @param query              A Person object with the query as interpreted by Pipl.
         * @param person             A Person object with data about the person in the query.
         * @param records            A list of Record objects with full/partial match to the
         *                           query.
         * @param suggestedSearches  A list of Record objects, each of these is an
         *                           expansion of the original query, giving additional
         *                           query parameters to zoom in on the right person.
         * @param warnings           A list of strings. A warning is returned when the query
         *                           contains a non-critical error and the search can still run.
         */
        public SearchAPIResponse(Person query = null, Person person = null, List<Record> records = null,
                                 List<Record> suggestedSearches = null, List<string> warnings = null)
        {
            this.Query = query;
            this.Person = person;
            this.Records = records;
            this.SuggestedSearches = suggestedSearches;
            this.Warnings = warnings;
        }

        /**
         * @return Records that match all the params in the query.
         */
        public List<Record> queryParamsMatchedRecords() {
            List<Record> matched = new List<Record>();
            foreach (Record r in this.Records) {
                if ((bool)r.QueryParamsMatch) {
                    matched.Add(r);
                }
            }
            return matched;
        }

        /**
            * @return Records that match the person from the query.
            *         Note that the meaning of "match the person from the query" means "Pipl
            *         is convinced that these records hold data about the person you're
            *         looking for".
            *         Remember that when Pipl is convinced about which person you're looking
            *         for, the response also contains a Person object. This person is
            *         created by merging all the fields and sources of these records.
            */
        public List<Record> queryPersonMatchedRecords() {
            List<Record> matched = new List<Record>();
            foreach (Record r in this.Records) {
                if (r.QueryPersonMatch == 1.0) {
                    matched.Add(r);
                }
            }
            return matched;
        }

        /**
            * @return Return the records grouped by the Domain they came from.
            *         <p/>
            *         The return value is a Dictionary, a key in this dictionary is a Domain
            *         and the value is a list of all the records with this Domain.
            */
        public Dictionary<string, List<Record>> groupRecordsByDomain() {
            Dictionary<string, List<Record>> map = new Dictionary<string, List<Record>>();
            foreach (Record record in Records) {
                string key = record.Source.Domain;
                if (map[key] == null) {
                    map[key] = new List<Record>();
                }
                map[key].Add(record);
            }
            return map;
        }

        /**
            * @return Return the records grouped by the category of their source.
            *         <p/>
            *         The return value is a Dictionary, a key in this dictionary is a category
            *         and the value is a list of all the records with this category.
            */
        public Dictionary<string, List<Record>> groupRecordsByCategory() {
            Dictionary<string, List<Record>> map = new Dictionary<string, List<Record>>();
            foreach (Record record in Records) {
                string key = record.Source.Category;
                if (map[key] == null) {
                    map[key] = new List<Record>();
                }
                map[key].Add(record);
            }
            return map;
        }

        /**
            * @return Return the records grouped by their query_params_match attribute.
            *         <p/>
            *         The return value is a Dictionary, a key in this dictionary is a queryParamsMatch
            *         bool (so the keys can be just true or false) and the value is a list
            *         of all the records with this queryParamsMatch value.
            */
        public Dictionary<bool, List<Record>> groupRecordsByQueryParamsMatch() {
            Dictionary<bool, List<Record>> map = new Dictionary<bool, List<Record>>();
            foreach (Record record in Records) {
                bool key = (bool)record.QueryParamsMatch;
                if (map[key] == null) {
                    map[key] = new List<Record>();
                }
                map[key].Add(record);
            }
            return map;
        }

        /**
            * @return Return the records grouped by their queryPersonMatch attribute.
            *         <p/>
            *         The return value is a Dictionary, a key in this dictionary is a queryPersonMatch
            *         float and the value is a list of all the records with this
            *         queryPersonMatch value.
            */
        public Dictionary<float, List<Record>> groupRecordsByQueryPersonMatch() {
            Dictionary<float, List<Record>> map = new Dictionary<float, List<Record>>();
            foreach (Record record in Records) {
                float key = (float)record.QueryPersonMatch;
                if (map[key] == null) {
                    map[key] = new List<Record>();
                }
                map[key].Add(record);
            }
            return map;
        }
    }
}
