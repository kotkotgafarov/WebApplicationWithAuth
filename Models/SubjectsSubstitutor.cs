using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class SubjectSubstitutor
    {
        public int SubjectId { get; set; }
        public int SubjectsSubstitutorId { get; set; }

        [NotMapped]
        public string SubjectName { get; set; }

        [NotMapped]
        public string SubjectsSubstitutorName { get; set; }

    }
}
