using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;
using Pipl.APIs.Data.Containers;
using Pipl.APIs.Utils;

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
     * - A list of sources (Pipl.APIs.Data.Containers.Source) that fully/partially
     * match the person from your query, if the query was for "Eric Cartman from
     * Colorado US" the response might also contain sources of "Eric Cartman
     * from US" (without Colorado).
     * <p/>
     * The response also contains the query as it was interpreted by Pipl. This
     * part is useful for verification and debugging, if some query parameters
     * were invalid you can see in response.query that they were ignored, you can
     * also see how the name/address from your query were parsed in case you
     * passed raw_name/raw_address in the query.
     * <p/>
     */
    public class SearchAPIResponse
    {
        [JsonProperty("query")]
        public Person Query { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("possible_persons")]
        public List<Person> PossiblePersons { get; set; }

        [JsonProperty("sources")]
        public List<Source> Sources { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        [JsonProperty("@search_id")]
        public string SearchId { get; set; }

        /**
         * @param query              A Person object with the query as interpreted by Pipl.
         * @param person             A Person object with data about the person in the query.
         * @param possible_persons   PossiblePersons
         * @param sources            A list of Source objects with full/partial match to the
         *                           query.
         * @param warnings           A list of strings. A warning is returned when the query
         *                           contains a non-critical error and the search can still run.
         * @param @search_id         string
         */
        public SearchAPIResponse(Person query = null, Person person = null, List<Person> possible_Persons = null, List<Source> sources = null,
                                 List<string> warnings = null, string searchId = null)
        {
            this.Query = query;
            this.Person = person;
            this.PossiblePersons = possible_Persons;
            this.Sources = sources;
            this.Warnings = warnings;
            this.SearchId = searchId;
        }

        /**
            * @return Return the sources grouped by the category of their source.
            *         <p/>
            *         The return value is a Lookup table, between categories and their sources
            */
        public ILookup<string, Source> GroupSourcesByCategory() 
        {
            return Sources.ToLookup(s => EnumExtensions.JsonEnumName(s.Category.Value));
        }

        public Address Address
        {
            get
            {
                if ((Person == null) || (Person.Addresses == null)) return null;
                return Person.Addresses.FirstOrDefault();
            }
        }

        public DOB DOB
        {
            get
            {
                if (Person == null) return null;
                return Person.DOB;
            }
        }

        public Education Education
        {
            get
            {
                if ((Person == null) || (Person.Educations == null)) return null;
                return Person.Educations.FirstOrDefault();
            }
        }

        public Email Email
        {
            get
            {
                if ((Person == null) || (Person.Emails == null)) return null;
                return Person.Emails.FirstOrDefault();
            }
        }

        public Ethnicity Ethnicitiy
        {
            get
            {
                if ((Person == null) || (Person.Ethnicities == null)) return null;
                return Person.Ethnicities.FirstOrDefault();
            }
        }

        public Gender Gender
        {
            get
            {
                if (Person == null) return null;
                return Person.Gender;
            }
        }

        public Image Image
        {
            get
            {
                if ((Person == null) || (Person.Images == null)) return null;
                return Person.Images.FirstOrDefault();
            }
        }

        public Job Job
        {
            get
            {
                if ((Person == null) || (Person.Jobs == null)) return null;
                return Person.Jobs.FirstOrDefault();
            }
        }

        public Language Language
        {
            get
            {
                if ((Person == null) || (Person.Languages == null)) return null;
                return Person.Languages.FirstOrDefault();
            }
        }

        public Name Name
        {
            get
            {
                if ((Person == null) || (Person.Names == null)) return null;
                return Person.Names.FirstOrDefault();
            }
        }

        public OriginCountry OriginCountry
        {
            get
            {
                if ((Person == null) || (Person.OriginCountries == null)) return null;
                return Person.OriginCountries.FirstOrDefault();
            }
        }

        public Phone Phone
        {
            get
            {
                if ((Person == null) || (Person.Phones == null)) return null;
                return Person.Phones.FirstOrDefault();
            }
        }

        public URL Url
        {
            get
            {
                if ((Person == null) || (Person.Urls == null)) return null;
                return Person.Urls.FirstOrDefault();
            }
        }

        public UserID UserID
        {
            get
            {
                if ((Person == null) || (Person.UserIDs == null)) return null;
                return Person.UserIDs.FirstOrDefault();
            }
        }

        public Username Username
        {
            get
            {
                if ((Person == null) || (Person.Usernames == null)) return null;
                return Person.Usernames.FirstOrDefault();
            }
        }

        public Relationship Relationship
        {
            get
            {
                if ((Person == null) || (Person.Relationships == null)) return null;
                return Person.Relationships.FirstOrDefault();
            }
        }
    }
}
