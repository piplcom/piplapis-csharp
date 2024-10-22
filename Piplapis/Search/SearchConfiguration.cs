﻿using System;

namespace Pipl.APIs.Search
{
    public class SearchConfiguration
    {
        public string ApiKey { get; set; }
        public float ApiVersion { get; set; }
        public float? MinimumProbability { get; set; }
        public ShowSources? ShowSources { get; set; }
        public bool? HideSponsored { get; set; }
        public bool? LiveFeeds { get; set; }
        public float? MinimumMatch { get; set; }
        public bool UseHttps { get; set; }
        public bool? InferPersons { get; set; }
        public string MatchRequirements { get; set; }
        public bool? TopMatch { get; set; }
        public string SourceCategoryRequirements { get; set; }
        public string Url { get; set; }
        /**
         * @param ApiKey            A valid API key
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
         * @param minimum_match     float? value range: 0-1 (default is None).
         * @param useHttps          Always true, HTTP is forbidden.
         *      
         * @param inferPersons      boolean (default False),  whether the API should return person responses 
         *                          made up solely from data inferred by statistical analysis.
         * @param matchRequirements String: a match requirements criteria. This criteria defines what fields
         *                          must be present in an API response in order for it to be returned as a match. default null
         *                          
         * @param sourceCategoryRequirements: String: A source category requirements criteria. This criteria defines
         *                          what source categories must be present in an API response in order for it to be
         *                          returned as a match. For example: "personal_profiles" or "personal_profiles or professional_and_business"
         */

        public static string defaultApiKey = "";
        public static string defaultApiUrl = "api.pipl.com/search/";
        private static float defaultApiVersion = 5;

        protected string GetApiKey(string apiKey){
            if(apiKey != null){
                return apiKey;
            }

            string apiEnvKey = Environment.GetEnvironmentVariable("PIPL_API_KEY");

            if(apiEnvKey != null){
                return apiEnvKey;
            }

            return SearchConfiguration.defaultApiKey;
        }

        protected string GetUrl(string url){
            if(url != null){
                return url;
            }

            string apiUrl = Environment.GetEnvironmentVariable("PIPL_API_URL");

            if(apiUrl != null){
                return apiUrl;
            }

            return SearchConfiguration.defaultApiUrl;
        }

        protected float GetApiVersion(float? apiVersion){
            if(apiVersion != null){
                return (float) apiVersion;
            }

            string apiVersionString = Environment.GetEnvironmentVariable("PIPL_API_VERSION");

            if(apiVersionString != null){
                return float.Parse(apiVersionString);
            }

            return SearchConfiguration.defaultApiVersion;
        }

        public SearchConfiguration(
            string apiKey = null,
            float? minimumProbability = null,
            ShowSources? showSources = null,
            bool? hideSponsored = null, 
            bool? liveFeeds = null, 
            float? minimumMatch = null,
            bool useHttps = true, 
            string matchRequirements = null, 
            bool? inferPersons = false,
            string sourceCategoryRequirements = null, 
            string url = null, 
            bool? topMatch = false,
            float? apiVersion = null
        ){
            this.ApiKey = this.GetApiKey(apiKey);
            this.MinimumProbability = minimumProbability;
            this.ShowSources = showSources;
            this.HideSponsored = hideSponsored;
            this.LiveFeeds = liveFeeds;
            this.MinimumMatch = minimumMatch;
            this.UseHttps = true;
            this.InferPersons = inferPersons;
            this.MatchRequirements = matchRequirements;
            this.TopMatch = topMatch;
            this.SourceCategoryRequirements = sourceCategoryRequirements;
            this.Url = this.GetUrl(url);
            this.ApiVersion = GetApiVersion(apiVersion);
        }
    }
}
