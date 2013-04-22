using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pipl.APIs.Search
{
    /**
     * An exception raised when the response from the search API contains an error.
     */
    public class SearchAPIError : APIError
    {
        public SearchAPIError(string error, int httpStatusCode) : base(error, httpStatusCode, null)
        {
        }
    }
}
