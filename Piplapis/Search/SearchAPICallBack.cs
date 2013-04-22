using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pipl.APIs.Search
{
    /**
    * Call back class for Asynchronous send
    * In case of successful execution exception callback will be called with response.
    * In case of failure errback will be called with exception.
    */
    public interface SearchAPICallBack
    {
        void callback(SearchAPIResponse searchAPIResponse);
        void errback(Exception exception);
    }
}
