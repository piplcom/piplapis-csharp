using Microsoft.VisualBasic.CompilerServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Pipl.APIs.Data.Enums;
using Pipl.APIs.Utils;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A vehicle number of a person.
     */
    public class Vehicle : Field
    {
        [JsonProperty("vin")]
        public string? Vin { get; set; }

        [JsonProperty("year")]
        public string? Year { get; set; }

        [JsonProperty("make")]
        public string? Make { get; set; }

        [JsonProperty("model")]
        public string? Model { get; set;}

        [JsonProperty("color")]
        public string? Color { get; set;}

        [JsonProperty("vehicle_type")]
        public string? VehicleType { get; set;}

        [JsonProperty("display")]
        public string Display { get; private set; }


        /**
         * @param vin - the vin number
         * @param year - the year of the vehicle
         * @param make - the make of the vehicle
         * @param model - the model of the vehicle
         * @param color - the color of the vehicle
         * @param vehicleType - the type of the vehicle
         */
        public Vehicle(
            string? vin = null,
            string? year = null,
            string? make = null,
            string? model = null,
            string? color = null,
            string? vehicleType = null
        ){
            this.Vin = vin;
            this.Year = year;
            this.Make = make;
            this.Model = model;
            this.Color = color;
            this.VehicleType = vehicleType;
            this.Display = this.GetDisplay();
        }


        protected string GetDisplay(){
            List<string> vals = new List<string>();


            if (!String.IsNullOrEmpty(this.Year)){
                vals.Add(this.Year);
            }

            if (!String.IsNullOrEmpty(this.Make)){
                vals.Add(this.Make);
            }

            if (!String.IsNullOrEmpty(this.Model)){
                vals.Add(this.Model);
            }

            if (!String.IsNullOrEmpty(this.VehicleType)){
                vals.Add(this.VehicleType);
            }

            if (!String.IsNullOrEmpty(this.Color)){
                vals.Add(this.Color);
            }

            if (vals != null && vals.Any()){
                vals.Add("-");
            }

            vals.Add("VIN");

            if (!String.IsNullOrEmpty(this.Vin)){
                vals.Add(this.Vin);
            }

            return String.Join(" ", vals);

        }

        public static bool ValidateVinChecksum(string vin){
            int checksum = 0;
            string checkDigit;
            string checksumChar;
            Dictionary<string, List<string>> replaceMap;
            List<int> positionalWeights;

            vin = vin.ToLower();
            checkDigit = Char.ToString(vin[8]);
            replaceMap = new Dictionary<string, List<string>>(){
               {"1", new List<string>{"a", "j"}},
               {"2", new List<string>{"b", "k", "s"}},
               {"3", new List<string>{"c", "l", "t"}},
               {"4", new List<string>{"d", "m", "u"}},
               {"5", new List<string>{"e", "n", "v"}},
               {"6", new List<string>{"f", "w"}},
               {"7", new List<string>{"g", "p", "x"}},
               {"8", new List<string>{"h", "y"}},
               {"9", new List<string>{"r", "z"}},
            };

            positionalWeights = new List<int>{8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2};

            foreach(var key in replaceMap.Keys){
                foreach(var val in replaceMap[key]){
                    vin = vin.Replace(val, key);
                }
            }

            for(int index = 0; index < vin.Length; index++){
                if(index == 8){
                    continue;
                }
                
                int currentNumber = Int32.Parse(vin[index].ToString());
                int result = currentNumber * positionalWeights[index];

                checksum += result;
            }

            checksum %= 11;
            
            
            if (checksum == 10){
               checksumChar = "x";
            }else{
                checksumChar = checksum.ToString();
            }
            
            return checksumChar == checkDigit;
        }


        public static bool IsVinValid(string vin){
            bool condition = (
                !String.IsNullOrEmpty(vin) &&
                vin.Length == 17 &&
                vin.ToLower().IndexOfAny("ioq".ToCharArray()) != -1 &&
                "uz0".IndexOfAny(new char[] {vin.ToLower()[9]}) != -1 &&
                Utils.IsAlpheNumeric(vin) &&
                Vehicle.ValidateVinChecksum(vin)
            );
            

            return condition;
        }

        /**
         * A bool value that indicates whether the address is a valid address to
         * search by.
         * 
         * @return bool
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return Vehicle.IsVinValid(this.Vin);
            }
        }


        public override string ToString()
        {
            return this.Display;
        }
    }
}
