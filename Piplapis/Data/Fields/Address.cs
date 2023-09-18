using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Pipl.APIs.Data.Enums;

namespace Pipl.APIs.Data.Fields
{
    /**
     * An address of a person.
     */
    public class Address : Field
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("po_box")]
        public string PoBox { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("house")]
        public string House { get; set; }

        [JsonProperty("apartment")]
        public string Apartment { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("@type")]
        public AddressTypes? Type { get; set; }

        [JsonProperty("display")]
        public string Display { get; private set; }

        /**
         * @param country
         *            `Country` and `State` are Country code (like "US") and State
         *            code (like "NY"), note that the full value is available as
         *            address.CountryFull and address.StateFull.
         * @param state
         *            `Country` and `State` are Country code (like "US") and State
         *            code (like "NY"), note that the full value is available as
         *            address.CountryFull and address.StateFull.
         * @param city
         *            City
         * @param poBox
         *            PoBox
         * @param street
         *            Street
         * @param house
         *            House
         * @param apartment
         *            Apartment
         * @param raw
         *            `raw` is an unparsed address like "123 Marina Blvd, San
         *            Francisco, California, US", useful when you want to search by
         *            address and don't want to work hard to parse it. Note that in
         *            response data there's never address.Raw, the addresses in the
         *            response are always parsed, this is only for querying with an
         *            unparsed address.
         * @param type
         *            Type is one of "home", "work", "old"
         * @param zip_code
         *            ZipCode
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public Address(string country = null, string state = null, string city = null, string poBox = null,
                string street = null, string house = null, string apartment = null, string raw = null,
                AddressTypes? type = null, string zip_code = null, string? validSince = null)
            : base(validSince)
        {
            this.Country = country;
            this.State = state;
            this.City = city;
            this.PoBox = poBox;
            this.Street = street;
            this.House = house;
            this.Apartment = apartment;
            this.Raw = raw;
            this.Type = type;
            this.ZipCode = zip_code;
        }

        public override string ToString()
        {
            return Display;
        }

        /**
         * @return A bool value that indicates whether the address is a valid
         *         address to search by.
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return !(String.IsNullOrEmpty(Raw) && String.IsNullOrEmpty(State) && String.IsNullOrEmpty(Country) && String.IsNullOrEmpty(City));
            }

        }

        /**
         * @return A bool value that indicates whether the address is a valid
         *         address to search by as sole given field
         */
        [JsonIgnore]
        public bool IsSoleSearchable
        {
            get
            {
                return !(String.IsNullOrEmpty(Raw) && (String.IsNullOrEmpty(City) || String.IsNullOrEmpty(Street) || String.IsNullOrEmpty(House)));
            }

        }

        /**
         * @return A bool value that indicates whether the object's Country is a
         *         valid Country code.
         */
        [JsonIgnore]
        public bool IsValidCountry
        {
            get
            {
                return !String.IsNullOrEmpty(Country)
                        && Utils.Countries.ContainsKey(Country.ToUpper());
            }
        }

        /**
         * @return A bool value that indicates whether the object's State is a valid
         *         State code.
         */
        [JsonIgnore]
        public bool IsValidState
        {
            get
            {
                return IsValidCountry
                    && Utils.States.ContainsKey(Country.ToUpper())
                    && !String.IsNullOrEmpty(State)
                    && Utils.States[Country.ToUpper()].ContainsKey(State.ToUpper());
            }

        }

        /**
         * @return the full name of the object's Country. example:
         *         <p>
         *         <blockquote>
         * 
         *         <pre>
         *             >>> Address address = new Address(country: "FR");
         *             >>> Console.WriteLine(address.getCountry());
         *             "FR"
         *             >>> Console.WriteLine(address.CountryFull);
         *             "France"
         *         <p/>
         * </pre>
         * 
         *         </blockquote> Output: France
         */
        [JsonIgnore]
        public string CountryFull
        {
            get
            {
                if (!IsValidCountry) return null;
                return Utils.Countries[Country.ToUpper()];
            }
        }

        /**
         * @return the full name of the object's State. example:
         *         <p>
         *         <blockquote>
         * 
         *         <pre>
         * 			>>> Address address = new Address(country: "US", state: "CO");
         * 				>>> Console.WriteLine(address.getState());
         * 				"CO"
         * 				>>> Console.WriteLine(address.StateFull);
         * 				"Colorado"
         *         <p/>
         * </pre>
         * 
         *         </blockquote> Output: Colorado
         */
        [JsonIgnore]
        public string StateFull
        {
            get
            {
                if (IsValidState)
                    return Utils.States[Country.ToUpper()][State.ToUpper()];
                return null;
            }
        }
    }
}
