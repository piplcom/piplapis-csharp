using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;

namespace Pipl.APIs.Name
{
    /**
     * Helper class for NameAPIResponse, holds alternate
     * first/Middle/last names for a name.
     */
    public class AltNames : Field
    {
        public List<String> First { get; set; }
        public List<String> Middle { get; set; }
        public List<String> Last { get; set; }

        public AltNames(List<String> first, List<String> middle, List<String> last) : base(default(DateTime))
        {
            this.First = first;
            this.Middle = middle;
            this.Last = last;
        }
    }
}
