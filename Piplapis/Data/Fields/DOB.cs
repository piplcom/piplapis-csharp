using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Date-of-birth of A person.
     * <p/>
     * Comes as a date-range (the exact date is within the range, if the exact
     * date is known the range will simply be with start=end).
     */
    public class DOB : Field
    {
        [JsonProperty("date_range")]
        public DateRange DateRange { get; set; }

        /**
         * @param validSince `validSince` is a <code>Date</code> object, it's the first time Pipl's
         *                   crawlers found this data on the page.
         * @param dateRange  `dateRange` is A DateRange object (Pipl.APIs.Data.Fields.DateRange),
         *                   the date-of-birth is within this range.
         */
        public DOB(DateRange dateRange = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.DateRange = dateRange;
        }

        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return DateRange != null;
            }
        }

        /**
         * Note: in a DOB object the Display is the estimated Age.
         */
        public override string ToString()
        {
            return Age.ToString();
        }

        /**
         * The estimated Age of the person.
         * <p/>
         * Note that A DOB object is based on a date-range and the exact date is
         * usually unknown so for Age calculation the the Middle of the range is
         * assumed to be the real date-of-birth.
         *
         * @return Age
         */
        [JsonIgnore]
        public int Age
        {
            get
            {
                DateTime dob = this.DateRange.Middle;
                DateTime now = DateTime.Now;
                if (dob.CompareTo(now) > 0)
                {
                    throw new System.ArgumentException("Can't be born in the future");
                }
                int year1 = now.Year;
                int year2 = dob.Year;
                int age = year1 - year2;
                int month1 = now.Month;
                int month2 = dob.Month;
                if (month2 > month1)
                {
                    age--;
                }
                else if (month1 == month2)
                {
                    int day1 = DateTime.DaysInMonth(now.Year, now.Month);
                    int day2 = DateTime.DaysInMonth(dob.Year, dob.Month);
                    if (day2 > day1)
                    {
                        age--;
                    }
                }
                return age;
            }
        }

        /**
         * A tuple of two ints - the minimum and maximum Age of the person.
         *
         * @return <code>Tuple</code> object - example : (10, 55)
         */
        [JsonIgnore]
        public Tuple<int, int> AgeRange
        {
            get
            {
                if (DateRange == null)
                {
                    return new Tuple<int, int>(0, 0);
                }
                else
                {
                    int x1 = DOB.FromBirthDate(DateRange.End).Age;
                    int x2 = DOB.FromBirthDate(DateRange.Start).Age;
                    return new Tuple<int, int>(x1, x2);
                }
            }
        }


        /**
         * Take a person's birth year (int) and return a new DOB object
         * suitable for him.
         *
         * @param birthYear
         * @return <code>DOB</code> object
         */

        public static DOB FromBirthYear(int birthYear)
        {
            if (birthYear < 0)
            {
                throw new ArgumentException("birth year must be positive");
            }
            return new DOB(dateRange: DateRange.FromYearsRange(birthYear, birthYear));
        }


        /**
         * Take a person's birth date <code>DateTime</code> and return a new DOB
         * object suitable for him.
         *
         * @param birthDate <code>DateTime</code> object
         * @return <code>DOB</code> object
         */
        public static DOB FromBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                throw new ArgumentException("birth_date can't be in the future");
            }
            return new DOB(dateRange: new DateRange(birthDate, birthDate));
        }


        /**
         * Take a person's Age (int) and return a new DOB object
         * suitable for him.
         *
         * @param Age Age
         * @return <code>DOB</code> object
         */
        public static DOB fromAge(int age)
        {
            return DOB.FromAgeRange(age, age);
        }


        /**
         * Take a person's minimal and maximal Age and return a new DOB object
         * suitable for him.
         *
         * @param start minimum Age
         * @param end   maximum Age
         * @return <code>DOB</code> object
         */
        public static DOB FromAgeRange(int start, int end)
        {
            if (start < 0 || end < 0)
            {
                throw new ArgumentException("start age and end age can't be negative");
            }
            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }
            DateTime low = DateTime.Now.AddYears(-end);
            DateTime high = DateTime.Now.AddYears(-start);
            return new DOB(dateRange: new DateRange(low, high));
        }
    }
}
