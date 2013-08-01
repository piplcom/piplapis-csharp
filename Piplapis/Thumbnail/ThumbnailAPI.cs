using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Search;
using Pipl.APIs.Data.Fields;

namespace Pipl.APIs.Thumbnail
{
    /**
     * C# wrapper for Pipl's Thumbnail API.
     * <p/>
     * Pipl's thumbnail API provides a thumbnailing service for presenting images in
     * your application. The images can be from the results you got from our Search
     * API but it can also be any web URI of an image.
     * <p/>
     * The thumbnails returned by the API are in the height/width defined in the
     * request. Additional features of the API are: 
     * - Detect and Zoom-in on human faces (in case there's a human face in the image).
     * - Optionally adding to the thumbnail the favicon of the website where the image is from (for
     *   attribution, recommended for copyright reasons).
     * <p/>
     * This class contains only one method - GenerateThumbnailUrl() that can be
     * used for transforming an image URL into a thumbnail API URL.
     */
    public class ThumbnailAPI
    {
        public static string BASE_URL = "http://api.pipl.com/thumbnail/v2/?";
        // HTTPS is also supported:
        //public static string BASE_URL = "https://api.pipl.com/thumbnail/v2/?";
        public static String defaultApiKey = null;
	    public static int MAX_PIXELS = 500;

	    /**
	     * Take an image URL and generate a thumbnail URL for that image. 
         *
         * Example(thumbnail URL from an image URL):
	     * <p>
	     * <blockquote>
	     * 
	     * <pre>
	     *  string imageUrl = "http://a7.twimg.com/a/ab76f.jpg";
	     *  ThumbnailAPI.GenerateThumbnailUrl(imageUrl, 100, 100, "twitter.com", "samplekey")
	     *  
         *  Output :
	     * "http://api.pipl.com/thumbnail/v2/?key=samplekey&
	     * favicon_domain=twitter.com&height=100&width=100&zoom_face=True&
	     * image_url=http%3A%2F%2Fa7.twimg.com%2Fa%2Fab76f.jpg"
	     * <p/>
	     * </pre>
	     * 
	     * </blockquote>
	     * <p/>
	     * Example (thumbnail URL from a record that came in the response of our
	     * Search API):
	     * <p>
	     * <blockquote>
	     * 
	     * <pre>
	     * GenerateThumbnailUrl(record.Images[0].Url, 100, 100, record.Source.Domain, "samplekey")
	     *
         * Output:
	     * "http://api.pipl.com/thumbnail/v2/?key=samplekey&
	     * favicon_domain=twitter.com&height=100&width=100&zoom_face=True&
	     * image_url=http%3A%2F%2Fa7.twimg.com%2Fa%2Fab76f.jpg"
	     * <p/>
	     * </pre>
	     * 
	     * </blockquote>
	     * 
	     * @param imageUrl
	     *            URL of the image you want to thumbnail.
	     * @param height
	     *            Requested thumbnail height in pixels, maximum 500
	     * @param width
	     *            Requested thumbnail width in pixels, maximum 500.
	     * @param faviconDomain
	     *            Optional, the Domain of the website where the image came from,
	     *            the favicon will be added to the corner of the thumbnail,
	     *            recommended for copyright reasones. 
         *            IMPORTANT: Don't assume that the Domain of the website is the Domain from `imageUrl`,
	     *            it's possible that domain1.com hosts its images on domain2.com.
	     * @param zoomFace
	     *            Indicates whether you want the thumbnail to zoom on the
	     *            face in the image (in case there is a face) or not.
	     * @param apiKey
	     *            A valid API key (use "samplekey" for experimenting). 
         *            Note that you can set a default API key
	     *            (Pipl.APIs.ThumbnailAPI.defaultApiKey = '<your_key>') instead of
	     *            passing your key in each call.
	     * @return
	     * @throws ArgumentException
	     *             is thrown in case of illegal parameters.
	     */
	    public static string GenerateThumbnailUrl(string imageUrl, int height,
			    int width, string faviconDomain = null, bool zoomFace = true, string apiKey = null)
        {
            if (String.IsNullOrEmpty(apiKey) && String.IsNullOrEmpty(ThumbnailAPI.defaultApiKey))
            {
			    throw new ArgumentException("a valid API key is required");
		    }
		    if (!new Image(url: imageUrl).IsValidUrl) {
			    throw new ArgumentException("imageUrl is not a valid URL");
		    }
		    if (!(0 < height && height <= MAX_PIXELS && 0 < width && width <= MAX_PIXELS)) {
			    throw new ArgumentException(
					"height/width must be between 0 and " + MAX_PIXELS);
		    }
            string key = String.IsNullOrEmpty(apiKey) ? SearchAPIRequest.defaultApiKey : apiKey;
		    if (faviconDomain == null) {
			    faviconDomain = "";
		    }
		    return String.Format("{0}key={1}&image_url={2}&height={3}&width={4}&favicon_domain={5}&zoom_face={6}",
                    BASE_URL,
				    Uri.EscapeDataString(key), 
                    Uri.EscapeDataString(imageUrl),
                    Uri.EscapeDataString(height.ToString()),
                    Uri.EscapeDataString(width.ToString()), 
                    Uri.EscapeDataString(faviconDomain),
                    Uri.EscapeDataString(zoomFace.ToString()));
	    }
    }
}
