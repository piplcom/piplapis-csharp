using System.Collections.Generic;
using Newtonsoft.Json;
using Pipl.APIs.Data.Fields;

namespace Pipl.APIs.Data.Containers
{
    /**
     * The base class of Record and Person, made only for inheritance.
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

        [JsonProperty("dobs")]
        public List<DOB> DOBs { get; set; }

        [JsonProperty("related_urls")]
        public List<RelatedURL> RelatedURLs { get; set; }

        [JsonProperty("relationships")]
        public List<Relationship> Relationships { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

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
            DOBs = new List<DOB>();
            RelatedURLs = new List<RelatedURL>();
            Relationships = new List<Relationship>();
            Tags = new List<Tag>();
        }

        public FieldsContainer(List<Field> fields = null)
            : this()
        {
            if (fields != null)
                AddFields(fields);
        }

        public void AddFields(List<Field> fields)
        {
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
                    DOBs.Add((DOB)field);
                }
                else if (field.GetType() == typeof(RelatedURL))
                {
                    RelatedURLs.Add((RelatedURL)field);
                }
                else if (field.GetType() == typeof(Relationship))
                {
                    Relationships.Add((Relationship)field);
                }
                else if (field.GetType() == typeof(Tag))
                {
                    Tags.Add((Tag)field);
                }
            }
        }

        /**
         * @return A list with all the fields contained in this object.
         */
        public List<Field> AllFields
        {
            get
            {
                List<Field> fields = new List<Field>();
                fields.AddRange(Names);
                fields.AddRange(Addresses);
                fields.AddRange(Phones);
                fields.AddRange(Emails);
                fields.AddRange(Jobs);
                fields.AddRange(Educations);
                fields.AddRange(Images);
                fields.AddRange(Usernames);
                fields.AddRange(UserIDs);
                fields.AddRange(DOBs);
                fields.AddRange(RelatedURLs);
                fields.AddRange(Relationships);
                fields.AddRange(Tags);
                return fields;
            }
        }
    }
}
