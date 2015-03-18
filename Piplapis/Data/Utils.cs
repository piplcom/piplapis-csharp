using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data
{
    /**
     * The <code>Utils</code> class is an utility class which has static utility
     * methods.
     * <p/>
     */
    public static class Utils
    {
        private const string _countryString = "{\"BD\":\"Bangladesh\",\"WF\":\"Wallis And Futuna Islands\",\"BF\":\"Burkina Faso\",\"PY\":\"Paraguay\",\"BA\":\"Bosnia And Herzegovina\",\"BB\":\"Barbados\",\"BE\":\"Belgium\",\"BM\":\"Bermuda\",\"BN\":\"Brunei Darussalam\",\"BO\":\"Bolivia\",\"BH\":\"Bahrain\",\"BI\":\"Burundi\",\"BJ\":\"Benin\",\"BT\":\"Bhutan\",\"JM\":\"Jamaica\",\"BV\":\"Bouvet Island\",\"BW\":\"Botswana\",\"WS\":\"Samoa\",\"BR\":\"Brazil\",\"BS\":\"Bahamas\",\"JE\":\"Jersey\",\"BY\":\"Belarus\",\"BZ\":\"Belize\",\"RU\":\"Russian Federation\",\"RW\":\"Rwanda\",\"LT\":\"Lithuania\",\"RE\":\"Reunion\",\"TM\":\"Turkmenistan\",\"TJ\":\"Tajikistan\",\"RO\":\"Romania\",\"LS\":\"Lesotho\",\"GW\":\"Guinea-bissau\",\"GU\":\"Guam\",\"GT\":\"Guatemala\",\"GS\":\"South Georgia And South Sandwich Islands\",\"GR\":\"Greece\",\"GQ\":\"Equatorial Guinea\",\"GP\":\"Guadeloupe\",\"JP\":\"Japan\",\"GY\":\"Guyana\",\"GG\":\"Guernsey\",\"GF\":\"French Guiana\",\"GE\":\"Georgia\",\"GD\":\"Grenada\",\"GB\":\"Great Britain\",\"GA\":\"Gabon\",\"GN\":\"Guinea\",\"GM\":\"Gambia\",\"GL\":\"Greenland\",\"GI\":\"Gibraltar\",\"GH\":\"Ghana\",\"OM\":\"Oman\",\"TN\":\"Tunisia\",\"JO\":\"Jordan\",\"HR\":\"Croatia\",\"HT\":\"Haiti\",\"SV\":\"El Salvador\",\"HK\":\"Hong Kong\",\"HN\":\"Honduras\",\"HM\":\"Heard And Mcdonald Islands\",\"AD\":\"Andorra\",\"PR\":\"Puerto Rico\",\"PS\":\"Palestine\",\"PW\":\"Palau\",\"PT\":\"Portugal\",\"SJ\":\"Svalbard And Jan Mayen Islands\",\"VG\":\"Virgin Islands, British\",\"AI\":\"Anguilla\",\"KP\":\"North Korea\",\"PF\":\"French Polynesia\",\"PG\":\"Papua New Guinea\",\"PE\":\"Peru\",\"PK\":\"Pakistan\",\"PH\":\"Philippines\",\"PN\":\"Pitcairn\",\"PL\":\"Poland\",\"PM\":\"Saint Pierre And Miquelon\",\"ZM\":\"Zambia\",\"EH\":\"Western Sahara\",\"EE\":\"Estonia\",\"EG\":\"Egypt\",\"ZA\":\"South Africa\",\"EC\":\"Ecuador\",\"IT\":\"Italy\",\"AO\":\"Angola\",\"KZ\":\"Kazakhstan\",\"ET\":\"Ethiopia\",\"ZW\":\"Zimbabwe\",\"SA\":\"Saudi Arabia\",\"ES\":\"Spain\",\"ER\":\"Eritrea\",\"ME\":\"Montenegro\",\"MD\":\"Moldova\",\"MG\":\"Madagascar\",\"MA\":\"Morocco\",\"MC\":\"Monaco\",\"UZ\":\"Uzbekistan\",\"MM\":\"Myanmar\",\"ML\":\"Mali\",\"MO\":\"Macau\",\"MN\":\"Mongolia\",\"MH\":\"Marshall Islands\",\"US\":\"United States\",\"UM\":\"United States Minor Outlying Islands\",\"MT\":\"Malta\",\"MW\":\"Malawi\",\"MV\":\"Maldives\",\"MQ\":\"Martinique\",\"MP\":\"Northern Mariana Islands\",\"MS\":\"Montserrat\",\"NA\":\"Namibia\",\"IM\":\"Isle Of Man\",\"UG\":\"Uganda\",\"MY\":\"Malaysia\",\"MX\":\"Mexico\",\"IL\":\"Israel\",\"BG\":\"Bulgaria\",\"FR\":\"France\",\"AW\":\"Aruba\",\"AX\":\"Åland Islands\",\"FI\":\"Finland\",\"FJ\":\"Fiji\",\"FK\":\"Falkland Islands\",\"FM\":\"Micronesia\",\"FO\":\"Faroe Islands\",\"NI\":\"Nicaragua\",\"NL\":\"Netherlands\",\"NO\":\"Norway\",\"SO\":\"Somalia\",\"NC\":\"New Caledonia\",\"NE\":\"Niger\",\"NF\":\"Norfolk Island\",\"NG\":\"Nigeria\",\"NZ\":\"New Zealand\",\"NP\":\"Nepal\",\"NR\":\"Nauru\",\"NU\":\"Niue\",\"MR\":\"Mauritania\",\"CK\":\"Cook Islands\",\"CI\":\"Côte D'Ivoire\",\"CH\":\"Switzerland\",\"CO\":\"Colombia\",\"CN\":\"China\",\"CM\":\"Cameroon\",\"CL\":\"Chile\",\"CC\":\"Cocos (keeling) Islands\",\"CA\":\"Canada\",\"CG\":\"Congo (brazzaville)\",\"CF\":\"Central African Republic\",\"CD\":\"Congo (kinshasa)\",\"CZ\":\"Czech Republic\",\"CY\":\"Cyprus\",\"CX\":\"Christmas Island\",\"CS\":\"Serbia\",\"CR\":\"Costa Rica\",\"HU\":\"Hungary\",\"CV\":\"Cape Verde\",\"CU\":\"Cuba\",\"SZ\":\"Swaziland\",\"SY\":\"Syria\",\"KG\":\"Kyrgyzstan\",\"KE\":\"Kenya\",\"SR\":\"Suriname\",\"KI\":\"Kiribati\",\"KH\":\"Cambodia\",\"KN\":\"Saint Kitts And Nevis\",\"KM\":\"Comoros\",\"ST\":\"Sao Tome And Principe\",\"SK\":\"Slovakia\",\"KR\":\"South Korea\",\"SI\":\"Slovenia\",\"SH\":\"Saint Helena\",\"KW\":\"Kuwait\",\"SN\":\"Senegal\",\"SM\":\"San Marino\",\"SL\":\"Sierra Leone\",\"SC\":\"Seychelles\",\"SB\":\"Solomon Islands\",\"KY\":\"Cayman Islands\",\"SG\":\"Singapore\",\"SE\":\"Sweden\",\"SD\":\"Sudan\",\"DO\":\"Dominican Republic\",\"DM\":\"Dominica\",\"DJ\":\"Djibouti\",\"DK\":\"Denmark\",\"DE\":\"Germany\",\"YE\":\"Yemen\",\"AT\":\"Austria\",\"DZ\":\"Algeria\",\"MK\":\"Macedonia\",\"UY\":\"Uruguay\",\"YT\":\"Mayotte\",\"MU\":\"Mauritius\",\"TZ\":\"Tanzania\",\"LC\":\"Saint Lucia\",\"LA\":\"Laos\",\"TV\":\"Tuvalu\",\"TW\":\"Taiwan\",\"TT\":\"Trinidad And Tobago\",\"TR\":\"Turkey\",\"LK\":\"Sri Lanka\",\"LI\":\"Liechtenstein\",\"LV\":\"Latvia\",\"TO\":\"Tonga\",\"TL\":\"Timor-leste\",\"LU\":\"Luxembourg\",\"LR\":\"Liberia\",\"TK\":\"Tokelau\",\"TH\":\"Thailand\",\"TF\":\"French Southern Lands\",\"TG\":\"Togo\",\"TD\":\"Chad\",\"TC\":\"Turks And Caicos Islands\",\"LY\":\"Libya\",\"VA\":\"Vatican City\",\"AC\":\"Ascension Island\",\"VC\":\"Saint Vincent And The Grenadines\",\"AE\":\"United Arab Emirates\",\"VE\":\"Venezuela\",\"AG\":\"Antigua And Barbuda\",\"AF\":\"Afghanistan\",\"IQ\":\"Iraq\",\"VI\":\"Virgin Islands, U.s.\",\"IS\":\"Iceland\",\"IR\":\"Iran\",\"AM\":\"Armenia\",\"AL\":\"Albania\",\"VN\":\"Vietnam\",\"AN\":\"Netherlands Antilles\",\"AQ\":\"Antarctica\",\"AS\":\"American Samoa\",\"AR\":\"Argentina\",\"AU\":\"Australia\",\"VU\":\"Vanuatu\",\"IO\":\"British Indian Ocean Territory\",\"IN\":\"India\",\"LB\":\"Lebanon\",\"AZ\":\"Azerbaijan\",\"IE\":\"Ireland\",\"ID\":\"Indonesia\",\"PA\":\"Panama\",\"UA\":\"Ukraine\",\"QA\":\"Qatar\",\"MZ\":\"Mozambique\"}";
        private const string _us = "{\"WA\":\"Washington\",\"VA\":\"Virginia\",\"DE\":\"Delaware\",\"DC\":\"District Of Columbia\",\"WI\":\"Wisconsin\",\"WV\":\"West Virginia\",\"HI\":\"Hawaii\",\"FL\":\"Florida\",\"YT\":\"Yukon\",\"WY\":\"Wyoming\",\"PR\":\"Puerto Rico\",\"NJ\":\"New Jersey\",\"NM\":\"New Mexico\",\"TX\":\"Texas\",\"LA\":\"Louisiana\",\"NC\":\"North Carolina\",\"ND\":\"North Dakota\",\"NE\":\"Nebraska\",\"FM\":\"Federated States Of Micronesia\",\"TN\":\"Tennessee\",\"NY\":\"New York\",\"PA\":\"Pennsylvania\",\"CT\":\"Connecticut\",\"RI\":\"Rhode Island\",\"NV\":\"Nevada\",\"NH\":\"New Hampshire\",\"GU\":\"Guam\",\"CO\":\"Colorado\",\"VI\":\"Virgin Islands\",\"AK\":\"Alaska\",\"AL\":\"Alabama\",\"AS\":\"American Samoa\",\"AR\":\"Arkansas\",\"VT\":\"Vermont\",\"IL\":\"Illinois\",\"GA\":\"Georgia\",\"IN\":\"Indiana\",\"IA\":\"Iowa\",\"MA\":\"Massachusetts\",\"AZ\":\"Arizona\",\"CA\":\"California\",\"ID\":\"Idaho\",\"PW\":\"Palau\",\"ME\":\"Maine\",\"MD\":\"Maryland\",\"OK\":\"Oklahoma\",\"OH\":\"Ohio\",\"UT\":\"Utah\",\"MO\":\"Missouri\",\"MN\":\"Minnesota\",\"MI\":\"Michigan\",\"MH\":\"Marshall Islands\",\"KS\":\"Kansas\",\"MT\":\"Montana\",\"MP\":\"Northern Mariana Islands\",\"MS\":\"Mississippi\",\"SC\":\"South Carolina\",\"KY\":\"Kentucky\",\"OR\":\"Oregon\",\"SD\":\"South Dakota\"}";
        private const string _ca = "{\"AB\":\"Alberta\",\"BC\":\"British Columbia\",\"MB\":\"Manitoba\",\"NB\":\"New Brunswick\",\"NT\":\"Northwest Territories\",\"NS\":\"Nova Scotia\",\"NU\":\"Nunavut\",\"ON\":\"Ontario\",\"PE\":\"Prince Edward Island\",\"QC\":\"Quebec\",\"SK\":\"Saskatchewan\",\"YU\":\"Yukon\",\"NL\":\"Newfoundland and Labrador\"}";
        private const string _au = "{\"WA\":\"State of Western Australia\",\"SA\":\"State of South Australia\",\"NT\":\"Northern Territory\",\"VIC\":\"State of Victoria\",\"TAS\":\"State of Tasmania\",\"QLD\":\"State of Queensland\",\"NSW\":\"State of New South Wales\",\"ACT\":\"Australian Capital Territory\"}";
        private const string _gb = "{\"WLS\":\"Wales\",\"SCT\":\"Scotland\",\"NIR\":\"Northern Ireland\",\"ENG\":\"England\"}";
        
        public readonly static Dictionary<string, string> Countries = new Dictionary<string, string>();
	    public readonly static Dictionary<string, Dictionary<string, string>> States = new Dictionary<string, Dictionary<string, string>>();
	    public const string CharsetName = "UTF-8";

        static Utils()
        {
            // Build the countries dictionary
            //
            Countries = JsonConvert.DeserializeObject<Dictionary<string, string>>(_countryString);

            // Build the states dictionary
            //
            States["US"] = JsonConvert.DeserializeObject<Dictionary<string, string>>(_us);
            States["CA"] = JsonConvert.DeserializeObject<Dictionary<string, string>>(_ca);
            States["AU"] = JsonConvert.DeserializeObject<Dictionary<string, string>>(_au);
            States["GB"] = JsonConvert.DeserializeObject<Dictionary<string, string>>(_gb);
        }

	    /**
	     * Return true if `url` (str/unicode) is a valid URL, false otherwise.
	     * 
	     * @param url       url to be validated.
	     * @return          <code>true</code> if the provided url is valid;
	     *                  <code>false</code> otherwise.
	     */
	    public static bool IsValidUrl(string url) {
            Uri uri = null;
            return !String.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.Absolute, out uri) ;
	    }

    }
}
