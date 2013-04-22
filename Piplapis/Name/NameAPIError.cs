using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pipl.APIs.Name
{
    /**
     * An exception raised when the response from the name API contains an
     * error.
     */
    public class NameAPIError : APIError
    {
        public NameAPIError(String error, int httpStatusCode)
            : base(error, httpStatusCode, null)
        {
        }

    }
}
