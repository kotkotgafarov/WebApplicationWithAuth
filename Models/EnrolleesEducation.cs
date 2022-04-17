using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EnrolleesEducation
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string NameOfSchool { get; set; }
        public int EnrolleeID { get; set; }
        public int EnrolleesDocId { get; set; }
        public int EducationLevelId { get; set; }

        [NotMapped]
        public string EducationLevelName { set; get; }
        [NotMapped]
        public string EnrolleesDocName { get; set; }

    }
}
