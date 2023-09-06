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
        public List<Data.Fields.Name>? Names { get; set; }

        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }

        [JsonProperty("phones")]
        public List<Phone>? Phones { get; set; }

        [JsonProperty("emails")]
        public List<Email>? Emails { get; set; }

        [JsonProperty("jobs")]
        public List<Job>? Jobs { get; set; }

        [JsonProperty("educations")]
        public List<Education>? Educations { get; set; }

        [JsonProperty("images")]
        public List<Image>? Images { get; set; }

        [JsonProperty("usernames")]
        public List<Username>? Usernames { get; set; }

        [JsonProperty("vehicles")]
        public List<Vehicle>? Vehicles { get; set; }

        [JsonProperty("user_ids")]
        public List<UserID>? UserIDs { get; set; }

        [JsonProperty("dob")]
        public DOB DOB { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("languages")]
        public List<Language>? Languages { get; set; }

        [JsonProperty("ethnicities")]
        public List<Ethnicity>? Ethnicities { get; set; }

        [JsonProperty("origin_countries")]
        public List<OriginCountry>? OriginCountries { get; set; }

        [JsonProperty("urls")]
        public List<URL>? Urls { get; set; }
        
        public FieldsContainer()
        {
            Names = null;
            Addresses = null;
            Phones = null;
            Emails = null;
            Jobs = null;
            Educations = null;
            Images = null;
            Usernames = null;
            Vehicles = null;
            UserIDs = null;
            Languages = null;
            Ethnicities = null;
            OriginCountries = null;
            Urls = null;
        }

        public FieldsContainer(IEnumerable<Field> fields = null)
            : this()
        {
            if (fields != null){
                AddFields(fields);
            }
        }

        public virtual void AddFields(IEnumerable<Field> fields)
        {
            if (fields == null){
                return;
            } 

            // Add the fields to their corresponding container.
            foreach (Field field in fields)
            {
                if (field.GetType() == typeof(Data.Fields.Name)){
                    Names ??= new List<Data.Fields.Name>();
                    Names.Add((Data.Fields.Name)field);

                    continue;
                }

                if (field.GetType() == typeof(Address)){
                    Addresses ??= new List<Address>();
                    Addresses.Add((Address)field);
                    
                    continue;
                }
                
                if (field.GetType() == typeof(Phone)){
                    Phones ??= new List<Phone>();
                    Phones.Add((Phone)field);

                    continue;
                }

                if (field.GetType() == typeof(Email)){
                    Emails ??= new List<Email>();
                    Emails.Add((Email)field);

                    continue;
                }

                if (field.GetType() == typeof(Job)){
                    Jobs ??= new List<Job>();
                    Jobs.Add((Job)field);

                    continue;
                }
                
                if (field.GetType() == typeof(Education)){
                    Educations ??= new List<Education>();
                    Educations.Add((Education)field);

                    continue;
                }
                
                if (field.GetType() == typeof(Image)){
                    Images ??= new List<Image>();
                    Images.Add((Image)field);

                    continue;
                }
                
                if (field.GetType() == typeof(Username)){
                    Usernames ??= new List<Username>();
                    Usernames.Add((Username)field);

                    continue;
                }
                
                if (field.GetType() == typeof(Vehicle)){
                    Vehicles ??= new List<Vehicle>();
                    Vehicles.Add((Vehicle)field);

                    continue;
                }
                
                if (field.GetType() == typeof(UserID)){
                    UserIDs ??= new List<UserID>();
                    UserIDs.Add((UserID)field);

                    continue;
                }
                
                if (field.GetType() == typeof(DOB)){
                    DOB = (DOB)field;

                    continue;
                }
                
                if (field.GetType() == typeof(Gender)){
                    Gender = (Gender)field;

                    continue;
                }
                
                if (field.GetType() == typeof(Language)){
                    Languages ??= new List<Language>();
                    Languages.Add((Language)field);

                    continue;
                }
                
                if (field.GetType() == typeof(Ethnicity)){
                    Ethnicities ??= new List<Ethnicity>();
                    Ethnicities.Add((Ethnicity)field);

                    continue;
                }
                
                if (field.GetType() == typeof(OriginCountry)){
                    OriginCountries ??= new List<OriginCountry>();
                    OriginCountries.Add((OriginCountry)field);

                    continue;
                }
                
                if (field.GetType() == typeof(URL)){
                    Urls ??= new List<URL>();
                    Urls.Add((URL)field);

                    continue;
                }
            }
        }

        /**
         * @return A list with all the fields contained in this object.
         */
        [JsonIgnore]
        public virtual IEnumerable<Field> AllFields
        {
            get
            {
                if (Names != null){
                    foreach (Field name in Names)
                    {
                        yield return name;
                    }
                }

                if (Addresses != null){
                   foreach (Field address in Addresses)
                    {
                        yield return address;
                    }
                }

                if (Phones != null){
                    foreach (Field phone in Phones)
                    {
                        yield return phone;
                    }
                }

                if (Emails != null){
                    foreach (Field email in Emails)
                    {
                        yield return email;
                    }
                }

                if (Educations != null){
                    foreach (Field education in Educations)
                    {
                        yield return education;
                    }
                }

                if (Usernames != null){
                    foreach (Field username in Usernames)
                    {
                        yield return username;
                    }
                }

                if (Vehicles != null){
                    foreach (Field vehicle in Vehicles)
                    {
                        yield return vehicle;
                    }
                }

                if (Images != null){
                    foreach (Field image in Images)
                    {
                        yield return image;
                    }
                }

                if (Languages != null){
                   foreach (Field language in Languages)
                    {
                        yield return language;
                    }
                }

                if (OriginCountries != null){
                    foreach (Field originCountry in OriginCountries)
                    {
                        yield return originCountry;
                    }
                }

                if (Ethnicities != null){
                    foreach (Field ethnicity in Ethnicities){
                        yield return ethnicity;
                    }
                }

                if (Jobs != null){
                    foreach (Field job in Jobs)
                    {
                        yield return job;
                    }
                }

                if (UserIDs != null){
                    foreach (Field userID in UserIDs)
                    {
                        yield return userID;
                    }
                }

                if (Urls != null){
                    foreach (Field url in Urls)
                    {
                        yield return url;
                    }
                }
                
                if (DOB != null){
                    yield return DOB;
                }

                if (Gender != null){
                    yield return Gender;
                } 
            }
        }
    }
}
