using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    public class YearOnlyDateTimeConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                writer.WriteValue(String.Format("{0:yyyy-MM-dd}", (DateTime)value));
//                writer.WriteValue(String.Format("{0:d/M/yyyy}", (DateTime)value));
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected String, got {0}.",
                    reader.TokenType));
            }

            var year = (string)reader.Value;

            return DateTime.Parse(year);
        }
    }
    /**
     * A time interval represented as a range of two dates.
     * DateRange objects are used inside DOB, Job and Education objects.
     */
    public class DateRange : Field, IEquatable<DateRange>
    {
        [JsonProperty("start")]
        [JsonConverter(typeof(YearOnlyDateTimeConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        [JsonConverter(typeof(YearOnlyDateTimeConverter))]
        public DateTime End { get; set; }

        /**
         * `start` and `end` are <code>DateTime</code> objects, both are required.
         * <p/>
         * For creating a DateRange object for an exact DateTime (like if exact
         * DateTime-of-birth is known) just pass the same value for `start` and `end`.
         *
         * @param start start DateTime
         * @param end   end DateTime
         */
        public DateRange(DateTime start = default(DateTime), DateTime end = default(DateTime))
        {
            if (start < end)
            {
                this.Start = start;
                this.End = end;
            }
            else
            {
                this.Start = end;
                this.End = start;
            }
        }

        public bool Equals(DateRange other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Start.Equals(Start) && other.End.Equals(End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DateRange)) return false;
            return Equals((DateRange) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start.GetHashCode()*397) ^ End.GetHashCode();
            }
        }

        public override string ToString()
        {
            return Start.ToString() + " - " + End.ToString();
        }

        /**
         * @return True if the object holds an exact DateTime (start=end),
         *         False otherwise.
         */
        [JsonIgnore]
        public bool IsExact
        {
            get
            {
                return Start == End;
            }
        }


        /**
         * @return The Middle of the DateTime range (a DateTime object).
         *         return self.start + (self.end - self.start) / 2
         */
        [JsonIgnore]
        public DateTime Middle
        {
            get
            {
                Int64 t1 = Start.Ticks;
                Int64 t2 = End.Ticks;
                return new DateTime(t1 + (t2 - t1) / 2);
            }
        }


        /**
         * @return A tuple of two ints - the year of the start DateTime and the year of the
         *         end DateTime.
         */
        [JsonIgnore]
        public Tuple<int, int> YearsRange
        {
            get
            {
                return new Tuple<int, int>(Start.Year, End.Year);
            }
        }


        /**
         * Transform a range of years (two ints) to a DateRange object.
         *
         * @param startYear startYear
         * @param endYear   endYear
         * @return <code>DateRange</code> object
         */
        public static DateRange FromYearsRange(int startYear, int endYear)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            DateTime end = new DateTime(startYear, 1, 1);

            // By adding the difference of the between the end year and the 
            // start year, we are getting the correct year but at January 1st.
            // We add to that one year to get the next year, and reducing one 
            // day to get the last day of the previous year.
            //
            end = end.AddYears(endYear - startYear + 1).AddDays(-1);
            return new DateRange(start, end);
        }
    }
}
