using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;
using Pipl.APIs.Data.Enums;

namespace Pipl.APIs.Data.Containers
{
    /**
     * A Source (used to be called Record) is all the data available in a specific source.
     * <p/>
     * Every source object is based on a source which is basically the URL of
     * the page where the data is available, and the data itself that comes as
     * field objects (Name, Address, Email etc. see Pipl.APIs.Data.Fields).
     * <p/>
     * Each type of field has its own container (note that Source is a subclass
     * of FieldsContainer). For example:
     * <p/>
     * Sources come as results for a query and therefore they have attributes
     * that indicate if and how much they match the query. They also have a
     * validity timestamp available as an attribute.
     */
    public class Source : FieldsContainer
    {
        [JsonProperty("@person_id")]
        public string PersonId { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@sponsored")]
        public bool? Sponsored { get; set; }

        [JsonProperty("@origin_url")]
        public string OriginUrl { get; set; }

        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("@category")]
        public SourceCategories? Category { get; set; }

        [JsonProperty("@domain")]
        public string Domain { get; set; }

        [JsonProperty("@match")]
        public float? Match { get; set; }

        [JsonProperty("premium")]
        public bool? Premium { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("relationships")]
        public List<Relationship> Relationships { get; set; }

        // TODO: still needed?
        [JsonProperty("@valid_since")]
        public string? ValidSince { get; set; }

        /**
         * @param fields             A List of <code>Field</code> objects
         * @param @person_id         GUID (only if this source belongs to matching person)
         * @param @id                the source id
         * @param @sponsered         Sponsered. if false omitted
         * @param @origin_url        OriginUrl. optional
         * @param @name              Name
         * @param @category          Category
         * @param @domain            Domain. optional
         * @param @Match             Match
         * @param @premium           Premium
         * @param validSince         A <code>DateTime</code> object, it's the first time Pipl's
         *                           crawlers found this data on the page.
         */
        public Source(IEnumerable<Field> fields, 
            string person_id, string id, bool sponsored, string origin_url, string name,
            SourceCategories? category, string domain, float match, bool premium,
            string validSince)
            : base(fields) 
        {
            Tags = new List<Tag>();
            Relationships = new List<Relationship>();
            AddFields(fields);

            this.PersonId = person_id;
            this.Id = id;
            this.Sponsored = sponsored;
            this.OriginUrl = origin_url;
            this.Name = name;
            this.Category = category;
            this.Domain = domain;
            this.Match = match;
            this.Premium = premium;
            this.ValidSince = validSince;
        }

        public void AddTags(IEnumerable<Tag> tags)
        {
            // Add the tags to their container.
            foreach (var t in tags)
            {
                Tags.Add(t);
            }
        }

        [JsonIgnore]
        public override IEnumerable<Field> AllFields
        {
            get
            {
                return base.AllFields.Concat(Tags.Cast<Field>());
            }
        }

        public override void AddFields(IEnumerable<Field> fields)
        {
            if (fields == null) return;

            var tags = fields.OfType<Tag>();

            AddTags(tags);

            base.AddFields(fields.Except(tags.Cast<Field>()));
        }
    }
}
