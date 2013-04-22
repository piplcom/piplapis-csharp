using System;
using System.Collections.Generic;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;

namespace Pipl.APIs.Data
{
    /**
     * A source of data that's available in a Record/Person object.
     * <p/>
     * The source is simply the URL of the page where the data was found, for
     * convenience it also contains some meta-data about the data-source (like its
     * full name and the category it belongs to).
     * <p/>
     */
    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("@is_sponsored")]
        public bool? IsSponsored { get; set; }

	    private static HashSet<string> categoriesSet = new HashSet<string> { "background_reports",
			    "contact_details", "email_address", "media", "personal_profiles",
			    "professional_and_business", "public_records", "publications",
			    "school_and_classmates", "web_pages" };

        /**
         * @param name
         *            name
         * @param category
         *            category is one of Source.categoriesSet.
         * @param url
         *            url
         * @param domain
         *            domain
         * @param isSponsored
         *            A bool value that indicates whether the
         *            source is from one of Pipl's sponsored sources.
         */
        public Source(string name = null, string category = null, string url = null, string domain = null, bool? isSponsored = null)
        {
            this.Name = name;
            this.Category = category;
            this.Url = url;
            this.Domain = domain;
            this.IsSponsored = isSponsored;
	    }

	    /**
	     * A bool that indicates whether the URL is valid.
	     * 
	     * @return <code>true</code> if the provided url is valid;
	     *         <code>false</code> otherwise.
	     */
        [JsonIgnore]
	    public bool IsValidUrl {
            get
            {
                return Utils.IsValidUrl(Url);
            }
	    }

	    /**
	     * Iterates over source categories and throws
	     * <code>ArgumentException</code> if any of them is invalid.
	     * 
	     * @param categories
	     *            Set of categories to be validated
	     * @throws ArgumentException
	     */
	    public static void ValidateCategories(HashSet<string> categories)
	    {
		    foreach (string cat in categories) {
			    if (!categoriesSet.Contains(cat)) {
				    throw new ArgumentException("Invalid categories: '"
						    + cat + "' is invalid");
			    }
		    }
	    }
    }
}
