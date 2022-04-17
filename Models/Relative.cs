using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class Relative
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int EnrolleeID { get; set; }

        [NotMapped]
        public bool Changed { set; get; }

    }
}
