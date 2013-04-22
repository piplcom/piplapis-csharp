using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * An email address of a person with the md5 of the address, might come in some
     * cases without the address itself and just the md5 (for privacy reasons)
     */
    public class Email : Field
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("address_md5")]
        public string AddressMd5 { get; set; }

        private static readonly HashSet<string> types = new HashSet<string> { "personal", "work" };

        [JsonProperty("@type")]
        private string type;

        private static readonly string myName = typeof (Email).Name;

        public string Type
        {
            set 
            {
                ValidateType(value, types, myName);
                this.type = value;
            }
            get { return type; }
        }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param address
         *            email address
         * @param addressMd5
         *            addressMd5
         * @param type
         *            type is one of "work", "personal".
         */
        public Email(string address = null, string addressMd5 = null, string type = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Address = address;
            this.AddressMd5 = addressMd5;
            this.Type = type;
        }

        /**
         * @return A bool value that indicates whether the Address is a valid email
         *         Address.
         */
        [JsonIgnore]
        public bool IsValidEmail
        {
            get
            {
                if (String.IsNullOrEmpty(this.Address)) return false;

                try
                {
                    var addr = new System.Net.Mail.MailAddress(this.Address);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /**
         * @return A bool value that indicates whether the Address is a valid
         *         Address to search by.
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return IsValidEmail;
            }
        }

        /**
         * @return The Username part of the email or null if the email is invalid.
         *         if Address is 'eric@cartman.com' the this method will return
         *         'eric'
         */
        [JsonIgnore]
        public string Username
        {
            get
            {
                if (IsValidEmail)
                {
                    return Address.Substring(0, Address.IndexOf("@"));
                }
                else
                {
                    return null;
                }
            }
        }

        /**
         * The Domain part of the email or null if the email is invalid. if Address
         * is "eric@cartman.com" then output will be "cartman.com"
         * 
         * @return Domain to which this email Address belongs.
         */
        [JsonIgnore]
        public string Domain
        {
            get
            {
                if (IsValidEmail)
                {
                    int start = Address.IndexOf("@") + 1;
                    return Address.Substring(start, Address.Length - start);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
