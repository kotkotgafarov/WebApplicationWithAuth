using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class ApplicationsAchievement
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public int EnrolleesDocId { get; set; }

        [NotMapped]
        public string EnrolleesDocName { get; set; }
    }
}
