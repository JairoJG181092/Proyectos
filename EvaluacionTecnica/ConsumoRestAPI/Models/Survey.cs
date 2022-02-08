using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvaluacionTecnica.Models
{
    public class Survey
    {
        public int IdSurvey { get; set; }
        public int IdActivity { get; set; }
        
        public DateTime created_at { get; set; }

        public ICollection<Answers> Answer{ get; set; }
    }
}
