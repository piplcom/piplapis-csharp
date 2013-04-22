using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;

namespace Pipl.APIs.Data.Containers
{
    /**
     * A Person object is all the data available on an individual.
     * <p/>
     * The Person object is essentially very similar in its structure to the Record
     * object, the main difference is that data about an individual can come from
     * multiple sources while a record is data from one source.
     * <p/>
     * The person's data comes as field objects (Name, Address, Email etc. see
     * Pipl.APIs.Data.Fields). Each type of field has its on container (note that
     * Person is a subclass of FieldsContainer).
     * </pre></blockquote>
     * <p/>
     * Note that a person object is used in the Search API in two ways: 
     * - It might come back as a result for a query (see SearchAPIResponse).
     * - It's possible to build a person object with all the information you already have about the
     *   person you're looking for and send this object as the query (see SearchAPIRequest).
     */
    public class Person : FieldsContainer
    {
        [JsonProperty("sources")]
        public List<Source> Sources { get; set; }

        [JsonProperty("@query_params_match")]
        public bool? QueryParamsMatch { get; set; }

        /**
         * @param fields             An List of <code>Field</code> objects
         * @param sources            A list of Source objects
         * @param queryParamsMatch 	 A bool value that indicates whether the record contains all the params
         *                           from the query or not.
         */
        public Person(List<Field> fields = null, List<Source> sources = null, bool? queryParamsMatch = null)
            : base(fields)
        {
            this.Sources = sources;
            this.QueryParamsMatch = queryParamsMatch;
        }

        /**
         * @return A bool value that indicates whether the person has enough data and
         *         can be sent as a query to the API.
         */
        [JsonIgnore]
        public bool IsSearchable
        {
            get
            {
                foreach (Fields.Name name in this.Names)
                {
                    if (name.IsSearchable)
                    {
                        return true;
                    }
                }
                foreach (Fields.Email email in this.Emails)
                {
                    if (email.IsSearchable)
                    {
                        return true;
                    }
                }
                foreach (Fields.Phone phone in this.Phones)
                {
                    if (phone.IsSearchable)
                    {
                        return true;
                    }
                }
                foreach (Fields.Username username in this.Usernames)
                {
                    if (username.IsSearchable)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /**
         * @return A list of all the fields that can't be searched by.
         *         For example: names/usernames that are too short, emails that are
         *         invalid etc.
         */
        [JsonIgnore]
        public List<Field> UnsearchableFields
        {
            get
            {
                List<Field> unSearchableList = new List<Field>();
                foreach (Field field in AllFields)
                {
                    if (!field.IsSearchable)
                    {
                        unSearchableList.Add(field);
                    }
                }
                return unSearchableList;
            }
        }
    }
}
