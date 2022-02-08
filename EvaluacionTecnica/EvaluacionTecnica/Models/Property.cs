using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Models
{
    public class Property
    {
        public int idproperty { get; set; }
        public string title { get; set; }
        public string addres { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime disabled_at { get; set; }
        public string status { get; set; }
        public bool dis { get; set; }
    }
}
