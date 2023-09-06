using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Enums;

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

        [JsonProperty("@disposable")]
        public bool? Disposable { get; set; }

        [JsonProperty("@email_provider")]
        public bool? EmailProvider { get; set; }

        [JsonProperty("@type")]
        public EmailTypes? Type { get; set; }

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
         * @param @disposable
         *            Disposable: is this a disposable email such as guerrillamail. 
         *            Only shown when true
         * @param @email_provider
         *            EmailProvider: is this a public provider such as gmail/outlook. 
         *            Only shown when true
         */
        public Email(
            string address = null,
            string addressMd5 = null, 
            EmailTypes? type = null, 
            bool? disposable = null, 
            bool? email_provider = null,
            string? validSince = null)
            : base(validSince)
        {
            this.Address = address;
            this.AddressMd5 = addressMd5;
            this.Type = type;
            this.Disposable = disposable;
            this.EmailProvider = email_provider;
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
                return (IsValidEmail) || (this.AddressMd5 != null && this.AddressMd5.Length == 32); ;
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

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Address)) 
                return this.Address;
            return "";
        }
    }
}
