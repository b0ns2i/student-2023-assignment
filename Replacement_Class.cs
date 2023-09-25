using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_2023_assignment
{
    public class Replacement_Class
    {

        public string Replacement { get; set; }
        public string Source { get; set; }
        public Replacement_Class(string replacement, string source)
        {
            Replacement = replacement;
            Source = source;
        }
    }
}
