using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EnrolleesStatus
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public int ProfileStatus { get; set; }
        public int ApplicationStatus { get; set; }

    }
}
