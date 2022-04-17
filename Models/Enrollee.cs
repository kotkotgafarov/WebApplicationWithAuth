using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class Enrollee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserID { get; set; }

        [NotMapped]
        public int ProfileStatus { get; set; }
        [NotMapped]
        public int ApplicationStatus { get; set; }
    }
}
