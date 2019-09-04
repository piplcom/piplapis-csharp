using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pipl.APIs.Search
{
    public class RequestConfiguration
    {
        public string ApiKey { get; set; }
        public float? MinimumProbability { get; set; }
        public ShowSources? ShowSources { get; set; }
        public bool? HideSponsored { get; set; }
        public bool? LiveFeeds { get; set; }
        public string SearchPointer { get; set; }
        public float? MinimumMatch { get; set; }
        public bool UseHttps { get; set; }

        /**
         * @param ApiKey            A valid API key (use "sample_key" for experimenting).
         *                          Note that you can set a default API key
         *                          (Pipl.APIs.Search.SearchAPIRequest.defaultApiKey = '<your_key>') instead of
         *                          passing it to each request object.
         * @param minimumProbability float (default 0.9).
         *                           The minimum acceptable probability for inferred data
         * @param showSources       String: false/all/matching (default false).
         *                          all - all sources are shown
         *                          matching - only sources from the person
         *                          false - don't show sources
         * @param hideSponsored     bool (default false).
         *                          Hide sponsored results.
         * @param liveFeeds         bool (default true).
         *                          Whether to use live search.
         * @param search_pointer    SearchPointer
         * @param minimum_match     float? value range: 0-1 (default is None).
         * @param useHttps          Always true
         */

        public const String DefaultApiKey = "sample_key";

        public RequestConfiguration(string apiKey = RequestConfiguration.DefaultApiKey,
                                float? minimumProbability = null, ShowSources? showSources = null,
                                bool? hideSponsored = null, bool? liveFeeds = null, 
                                string searchPointer = null, float? minimumMatch = null, 
                                bool useHttps = true)
        {
            this.ApiKey = apiKey;
            this.MinimumProbability = minimumProbability;
            this.ShowSources = showSources;
            this.HideSponsored = hideSponsored;
            this.LiveFeeds = liveFeeds;
            this.SearchPointer = searchPointer;
            this.MinimumMatch = minimumMatch;
            this.UseHttps = true;
        }
    }
}
