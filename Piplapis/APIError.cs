using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Pipl.APIs
{
    [Serializable]
    public class APIError : Exception
    {
        [JsonProperty("error")]
        public string Error { get; set; }
	    
        [JsonProperty("@http_status_code")]
        public int HttpStatusCode { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

	    public APIError(string error, int httpStatusCode, List<string> warnings) : base(error)
        {
		    this.Error = error;
		    this.HttpStatusCode = httpStatusCode;
            this.Warnings = warnings;
	    }

        public APIError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            Error = (string)info.GetValue("error", typeof(string));
            HttpStatusCode = (int)info.GetValue("@http_status_code", typeof(int));
            Warnings = (List<string>)info.GetValue("warnings", typeof(List<string>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue("error", this.Error);
                info.AddValue("@http_status_code", this.HttpStatusCode);
                info.AddValue("warnings", this.Warnings);
            }
        }
	    /**
	     * @return A bool that indicates whether the error is on the user's side.
	     */
	    public bool isUserError() {
            return 400 <= HttpStatusCode && HttpStatusCode < 500;
	    }

	    /**
	     * @return A bool that indicates whether the error is on Pipl's side.
	     */
	    public bool isPiplError() {
		    return !isUserError();
	    }
    }
}
