using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class ApplicationsContract
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public int EnrolleeId { get; set; }
        public int EnrolleesDocId { get; set; }

        [NotMapped]
        public string EnrolleesDocName { get; set; }
    }
}
