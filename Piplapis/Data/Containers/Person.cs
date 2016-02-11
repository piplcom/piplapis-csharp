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
     * The Person object is essentially very similar in its structure to the Source
     * object, the main difference is that data about an individual can come from
     * multiple sources while a Source is data from one source.
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
        [JsonProperty("relationships")]
        public List<Relationship> Relationships { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@match")]
        public float? Match { get; set; }

        [JsonProperty("@search_pointer")]
        public string SearchPointer { get; set; }

        [JsonProperty("@inferred")]
        public bool Inferred { get; set; }

        /**
         * @param fields             A List of <code>Field</code> objects
         * @param relationships      A List of <code>Relationship</code> objects
         * @param queryParamsMatch 	 A bool value that indicates whether the record contains all the params
         *                           from the query or not.
         * @param @id                GUID
         * @param @match             Match (float)
         * @param @search_pointer    SearchPointer - For PossiblePerson only
         */
        public Person(IEnumerable<Field> fields = null, bool? queryParamsMatch = null,
            string id = null, float? match = null, string search_pointer = null)
            : base(fields)
        {
            Relationships = new List<Relationship>();

            this.Id = id;
            this.Match = match;

            this.SearchPointer = search_pointer;
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
                if (SearchPointer != null)
                {
                    return true;
                }
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
        public IEnumerable<Field> UnsearchableFields
        {
            get
            {
                return AllFields.Where(f => !f.IsSearchable);
            }
        }
    }
}
