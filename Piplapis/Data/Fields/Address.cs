using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

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

        [JsonProperty("raw")]
        public string Raw { get; set; }

        private static readonly HashSet<string> types = new HashSet<string> { "home", "work", "old" };
        private static readonly string myName = typeof(Address).Name;

        [JsonProperty("@type")]
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                ValidateType(value, types, myName);
                this.type = value;
            }
        }

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
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public Address(string country = null, string state = null, string city = null, string poBox = null,
                string street = null, string house = null, string apartment = null, string raw = null,
                string type = null, DateTime? validSince = null)
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
        }

        [JsonProperty("display")]
        public string Display
        {
            get
            {
                string disp = "";
                List<string> vals = new List<string>();

                if (!String.IsNullOrEmpty(this.Street)) vals.Add(this.Street);

                if (!String.IsNullOrEmpty(this.City)) vals.Add(this.City);

                string state = String.IsNullOrEmpty(this.City) ? StateFull : this.State;
                if (!String.IsNullOrEmpty(state)) vals.Add(state);

                string country = String.IsNullOrEmpty(this.State) ? CountryFull : this.Country;
                if (!String.IsNullOrEmpty(country)) vals.Add(country);

                disp += String.Join(", ", vals);

                vals.Clear();
                if (!String.IsNullOrEmpty(this.Street))
                {
                    if (!String.IsNullOrEmpty(this.House)) vals.Add(this.House);
                    if (!String.IsNullOrEmpty(this.Apartment)) vals.Add(this.Apartment);
                    string prefix = String.Join("-", vals);
                    vals.Clear();
                    if (!String.IsNullOrEmpty(prefix)) vals.Add(prefix);
                    if (!String.IsNullOrEmpty(disp)) vals.Add(disp);
                    disp = String.Join(" ", vals);
                }
                else if (!String.IsNullOrEmpty(this.PoBox))
                {
                    disp = "P.O. Box " + this.PoBox + (String.IsNullOrEmpty(disp) ? "" : " ") + disp;
                }
                return disp;
            }
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
                return (!String.IsNullOrEmpty(Raw) || (IsValidCountry &&
                    (String.IsNullOrEmpty(this.State) || IsValidState)));
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
                        && Utils.COUNTRIES.ContainsKey(Country.ToUpper());
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
                    && Utils.STATES.ContainsKey(Country.ToUpper())
                    && !String.IsNullOrEmpty(State)
                    && Utils.STATES[Country.ToUpper()].ContainsKey(State.ToUpper());
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
                return Utils.COUNTRIES[Country.ToUpper()];
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
                    return Utils.STATES[Country.ToUpper()][State.ToUpper()];
                return null;
            }
        }
    }
}
