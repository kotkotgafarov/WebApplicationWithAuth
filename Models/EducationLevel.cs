using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EducationLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
