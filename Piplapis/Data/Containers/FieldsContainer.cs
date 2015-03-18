using System.Collections.Generic;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;
using System.Linq;

namespace Pipl.APIs.Data.Containers
{
    /**
     * The base class of Source and Person, made only for inheritance.
     */
    public class FieldsContainer
    {
        [JsonProperty("names")]
        public List<Data.Fields.Name> Names { get; set; }

        [JsonProperty("addresses")]
        public List<Address> Addresses { get; set; }

        [JsonProperty("phones")]
        public List<Phone> Phones { get; set; }

        [JsonProperty("emails")]
        public List<Email> Emails { get; set; }

        [JsonProperty("jobs")]
        public List<Job> Jobs { get; set; }

        [JsonProperty("educations")]
        public List<Education> Educations { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("usernames")]
        public List<Username> Usernames { get; set; }

        [JsonProperty("user_ids")]
        public List<UserID> UserIDs { get; set; }

        [JsonProperty("dob")]
        public DOB DOB { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty("ethnicities")]
        public List<Ethnicity> Ethnicities { get; set; }

        [JsonProperty("origin_countries")]
        public List<OriginCountry> OriginCountries { get; set; }

        [JsonProperty("urls")]
        public List<URL> Urls { get; set; }
        
        public FieldsContainer()
        {
            Names = new List<Data.Fields.Name>();
            Addresses = new List<Address>();
            Phones = new List<Phone>();
            Emails = new List<Email>();
            Jobs = new List<Job>();
            Educations = new List<Education>();
            Images = new List<Image>();
            Usernames = new List<Username>();
            UserIDs = new List<UserID>();
            Languages = new List<Language>();
            Ethnicities = new List<Ethnicity>();
            OriginCountries = new List<OriginCountry>();
            Urls = new List<URL>();
        }

        public FieldsContainer(IEnumerable<Field> fields = null)
            : this()
        {
            if (fields != null)
                AddFields(fields);
        }

        public virtual void AddFields(IEnumerable<Field> fields)
        {
            if (fields == null) return;

            // Add the fields to their corresponding container.
            foreach (Field field in fields)
            {
                if (field.GetType() == typeof(Data.Fields.Name))
                {
                    Names.Add((Data.Fields.Name)field);
                }
                else if (field.GetType() == typeof(Address))
                {
                    Addresses.Add((Address)field);
                }
                else if (field.GetType() == typeof(Phone))
                {
                    Phones.Add((Phone)field);
                }
                else if (field.GetType() == typeof(Email))
                {
                    Emails.Add((Email)field);
                }
                else if (field.GetType() == typeof(Job))
                {
                    Jobs.Add((Job)field);
                }
                else if (field.GetType() == typeof(Education))
                {
                    Educations.Add((Education)field);
                }
                else if (field.GetType() == typeof(Image))
                {
                    Images.Add((Image)field);
                }
                else if (field.GetType() == typeof(Username))
                {
                    Usernames.Add((Username)field);
                }
                else if (field.GetType() == typeof(UserID))
                {
                    UserIDs.Add((UserID)field);
                }
                else if (field.GetType() == typeof(DOB))
                {
                    DOB = (DOB)field;
                }
                else if (field.GetType() == typeof(Gender))
                {
                    Gender = (Gender)field;
                }
                else if (field.GetType() == typeof(Language))
                {
                    Languages.Add((Language)field);
                }
                else if (field.GetType() == typeof(Ethnicity))
                {
                    Ethnicities.Add((Ethnicity)field);
                }
                else if (field.GetType() == typeof(OriginCountry))
                {
                    OriginCountries.Add((OriginCountry)field);
                }
                else if (field.GetType() == typeof(URL))
                {
                    Urls.Add((URL)field);
                }
            }
        }

        /**
         * @return A list with all the fields contained in this object.
         */
        public virtual IEnumerable<Field> AllFields
        {
            get
            {
                var res = Names.Cast<Field>()
                    .Concat(Addresses).Cast<Field>()
                    .Concat(Phones).Cast<Field>()
                    .Concat(Emails).Cast<Field>()
                    .Concat(Jobs).Cast<Field>()
                    .Concat(Educations).Cast<Field>()
                    .Concat(Images).Cast<Field>()
                    .Concat(Usernames).Cast<Field>()
                    .Concat(UserIDs).Cast<Field>()
                    .Concat(Languages).Cast<Field>()
                    .Concat(Ethnicities).Cast<Field>()
                    .Concat(OriginCountries).Cast<Field>()
                    .Concat(Urls).Cast<Field>();

                foreach (var item in res)
                    yield return item;

                if (DOB != null) yield return DOB;
                if (Gender != null) yield return Gender;
            }
        }
    }
}
