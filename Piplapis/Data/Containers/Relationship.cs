using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Containers;
using Pipl.APIs.Data.Enums;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Name of another person related to this person.
     */
    public class Relationship : FieldsContainer
    {
        [JsonProperty("@type")]
        public RelationshipTypes? Type{ get; set; }

        [JsonProperty("@subtype")]
        public string Subtype { get; set; }

        /**
         * @param fields             
         *            A List of <code>Field</code> objects
         * @param type
         *            type is a string and one of "friend", "family", "work",
         *            "other"
         * @param subtype
         *            `subtype` is a string and is not restricted to a specific list
         *            of possible values (for example, if type_ is "family" then
         *            subtype can be "Father", "Mother", "Son" and many other
         *            things).
         */
        public Relationship(IEnumerable<Field> fields = null, 
            RelationshipTypes? type = null, string subtype = null)
            : base(fields)
            // TODO: removed ValidSince
        {
            this.Type = type;
            this.Subtype = subtype;
        }

        public override string ToString()
        {
            if (Names != null && Names.Count > 0)
                return Names[0].ToString();
            return "";
        }
             
    }
}
