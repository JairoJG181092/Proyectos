using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Models
{
    public class Activity
    {
        public int idactivity { get; set; }
        public int idProperty { get; set; }
        public string shedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        
    }
}
