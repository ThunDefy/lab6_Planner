using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_Planner.Models
{
    public class Plans
    {
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        
        public string Description { get; set; }

        public Plans(string t, string d, DateTimeOffset date)
        {
            Date = date;
            Title = t;
            Description = d;
        }
    }
}
