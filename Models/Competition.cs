using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Speciality { get; set; }
        public string Department { get; set; }
        public string Budget { get; set; }
        public string Form { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public bool Selected { set; get; }
    }
}
