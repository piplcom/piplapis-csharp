using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A general purpose element that holds any meaningful string that's related to
     * the person.
     * <p/>
     * Used for holding data about the person that either couldn't be clearly
     * classified or was classified as something different than the available data
     * fields.
     */
    public class Tag : Field
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("@classification")]
        public string Classification { get; set; }

	    /**
	     * Constructor
	     * 
	     * @param validSince
	     *            `validSince` is a <code>DateTime</code> object, it's the first
	     *            time Pipl's crawlers found this data on the page.
	     * @param content
	     *            `content` is the tag itself
	     * @param classification
	     *            classification of tag
	     */
        public Tag(string content = null, string classification = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
            this.Classification = classification;
	    }
    }
}
