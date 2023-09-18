using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Enums;
using Pipl.APIs.Utils;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Gender of a person.
     */
    public class Gender : Field
    {
        [JsonProperty("content")]
        public GenderTypes? Content { get; set; }

        /**
         * @param content
         *            Content ("male"/"female")
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public Gender(GenderTypes? content = null, string? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            if (Content == null)
                return "";
            return EnumExtensions.JsonEnumName(Content.Value);
        }
    }
}
