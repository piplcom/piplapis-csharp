using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;

namespace Pipl.APIs.Name
{
    /**
     * A response from Pipl's search API.
     * <p/>
     * A response contains the name from the query (parsed), and when available
     * the gender, nicknames, full-names, spelling options, translations, common
     * locations and common ages for the name. It also contains an estimated
     * number of people in the world with this name.
     */
    public class NameAPIResponse
    {
        [JsonProperty("name")]
        public Data.Fields.Name Name { get; set; }

        [JsonProperty("gender")]
        public List<object> GenderList { get; set; }

        public string Gender
        {
            get { return (string)GenderList[0]; }
            set { GenderList[0] = value; }
        }

        public double GenderConfidence
        {
            get { return Convert.ToDouble(GenderList[1]); }
            set { GenderList[1] = value; }
        }

        [JsonProperty("full_names")]
        public AltNames FullNames { get; set; }

        [JsonProperty("nicknames")]
        public AltNames Nicknames { get; set; }

        [JsonProperty("spellings")]
        public AltNames Spellings { get; set; }

        [JsonProperty("translations")]
        public Dictionary<string, AltNames> Translations { get; set; }

        [JsonProperty("top_locations")]
        public List<LocationStats> TopLocations { get; set; }

        [JsonProperty("top_ages")]
        public List<AgeStats> TopAges { get; set; }

        [JsonProperty("estimated_world_persons_count")]
        public int? EstimatedWorldPersonsCount { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        // IMPORTANT: This constructor is needed to initialize (create) the gender list of object 
        // for the json de/serializer, otherwise it doesn't recognize it using reflection.
        public NameAPIResponse() { }

        /**
         * @param name                       A Pipl.APIs.Data.Fields.Name object - the name from the query.
         * @param gender                     string, "male" or "female".
         * @param genderConfidence           float between 0.0 and 1.0, represents how
         *                                   confidence Pipl is that `gender` is the correct one.
         *                                   (Unisex names will get low confidence score).
         * @param fullNames                  An AltNames object.
         * @param nicknames                  An AltNames object.
         * @param spellings                  An AltNames object.
         * @param translations               A Dictionary of language_code -> AltNames object for this
         *                                   language.
         * @param topLocations               A list of LocationStats objects.
         * @param topAges                    A list of AgeStats objects.
         * @param estimatedWorldPersonsCount int, estimated number of people in the
         *                                   world with the name from the query.
         * @param warnings                   A list of strings. A warning is returned when the query
         *                                   contains a non-critical error.
         */
        public NameAPIResponse(Data.Fields.Name name = null, string gender = null, float? genderConfidence = null, AltNames fullNames = null,
                               AltNames nicknames = null, AltNames spellings = null,
                               Dictionary<string, AltNames> translations = null,
                               List<LocationStats> topLocations = null, List<AgeStats> topAges = null,
                               int? estimatedWorldPersonsCount = null, List<string> warnings = null)
        {
            this.Name = name;
            this.GenderList = new List<object>();
            this.GenderList.Add(gender);
            this.GenderList.Add(genderConfidence);
            this.FullNames = fullNames;
            this.Nicknames = nicknames;
            this.Spellings = spellings;
            this.Translations = translations;
            this.TopLocations = topLocations;
            this.TopAges = topAges;
            this.EstimatedWorldPersonsCount = estimatedWorldPersonsCount;
            this.Warnings = warnings;
        }
    }
}
