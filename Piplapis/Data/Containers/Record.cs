using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;

namespace Pipl.APIs.Data.Containers
{
    /**
     * A record is all the data available in a specific source.
     * <p/>
     * Every record object is based on a source which is basically the URL of
     * the page where the data is available, and the data itself that comes as
     * field objects (Name, Address, Email etc. see Pipl.APIs.Data.Fields).
     * <p/>
     * Each type of field has its own container (note that Record is a subclass
     * of FieldsContainer). For example:
     * <p/>
     * Records come as results for a query and therefore they have attributes
     * that indicate if and how much they match the query. They also have a
     * validity timestamp available as an attribute.
     */
    public class Record : FieldsContainer
    {
        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("@query_params_match")]
        public bool? QueryParamsMatch { get; set; }

        [JsonProperty("@query_person_match")]
        public float? QueryPersonMatch { get; set; }

        [JsonProperty("@valid_since")]
        public DateTime? ValidSince { get; set; }

        /**
         * @param fields             A List of <code>Field</code> objects
         * @param source             A Source object
         * @param queryParamsMatch   A bool value that indicates whether the record contains all the params from the
         *                           query or not.
         * @param queryPersonMatch   A float between 0.0 and 1.0 that
         *                           indicates how likely it is that this record holds data about the person
         *                           from the query. Higher value means higher likelihood, value of 1.0 means
         *                           "this is definitely him". This value is based on Pipl's statistical
         *                           algorithm that takes into account many parameters like the popularity of
         *                           the name/address (if there was a name/address in the query) etc.
         * @param validSince         A <code>DateTime</code> object, it's the first time Pipl's
         *                           crawlers found this data on the page.
         */
        public Record(List<Field> fields, Source source, bool queryParamsMatch, float queryPersonMatch,
                      DateTime validSince) : base(fields) 
        {
            this.Source = source;
            this.QueryParamsMatch = queryParamsMatch;
            this.QueryPersonMatch = queryPersonMatch;
            this.ValidSince = validSince;
        }
    }
}
