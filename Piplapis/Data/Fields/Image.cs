using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A URL of an image of a person.
     */
    public class Image : Field
    {
        private const int MinPixels = 100;
        private const int MaxPixels = 500;

        // HTTP URL
        private static string BaseUrlHttp  = "http://thumb.pipl.com/image?";
        // HTTPS is also supported:
        private static string BaseUrlHttpS = "https://thumb.pipl.com/image?";

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnail_token")]
        public string ThumbnailToken { get; set; }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param url
         *            url
         * @param thumbnail_token
         *            ThumbnailToken. token for thumbnail server
         */
        public Image(string url = null, string thumbnailToken = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Url = url;
            this.ThumbnailToken = thumbnailToken;
        }

        /**
         * @param width
         *            Requested thumbnail width in pixels, minimum 100, maximum 500.
         * @param height
         *            Requested thumbnail height in pixels, minimum 100, maximum 500
         * @param faviconDomain
         *            Optional, default is true
         *            the Domain of the website where the image came from,
         *            the favicon will be added to the corner of the thumbnail,
         *            recommended for copyright reasones. 
         *            IMPORTANT: Don't assume that the Domain of the website is the Domain from `imageUrl`,
         *            it's possible that domain1.com hosts its images on domain2.com.
         * @param zoomFace
         *            Optional, default is true
         *            Indicates whether you want the thumbnail to zoom on the
         *            face in the image (in case there is a face) or not.
         * @param useHttps
         *            Optional, default is false
         *            Indicates whether to use https(true) or http(false)
         * @return
         *            string with the thumbnail's url
         * @throws ArgumentException
         *             is thrown in case of illegal parameters.
         */
        public string GetThumbnailUrl(int width, int height, 
            bool? showFavicon = null, bool? zoomFace = null, 
            bool useHttps = false)
        {
            if (!(MinPixels <= height && height <= MaxPixels && MinPixels <= width && width <= MaxPixels))
            {
                throw new ArgumentException(
                    "height/width must be between " + MinPixels + " and " + MaxPixels);
            }

            string thumbnailUrl;
            string url;

            url = useHttps ? BaseUrlHttpS : BaseUrlHttp;

            string test = String.Format("{0}", ThumbnailToken);
            thumbnailUrl =
                String.Format("{0}&token={1}&width={2}&height={3}",
                    url,
                    ThumbnailToken,
                    Uri.EscapeDataString(width.ToString()),
                    Uri.EscapeDataString(height.ToString()));
            if (showFavicon != null)
                thumbnailUrl += String.Format("&favicon={0}", Uri.EscapeDataString(showFavicon.ToString()));
            if (zoomFace != null)
                thumbnailUrl += String.Format("&zoom_face={0}", Uri.EscapeDataString(zoomFace.ToString()));

            return thumbnailUrl;
        }

        [JsonIgnore]
        public bool IsValidUrl
        {
            get
            {
                return !String.IsNullOrEmpty(Url) && Utils.IsValidUrl(this.Url);
            }
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Url))
                return "";
            return this.Url;
        }
    }
}
