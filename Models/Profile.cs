using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithAuth.Models
{
    public class Profile
    {
        public Enrollee enrollee { get; set; }

        public EnrolleesDoc[] docs { get; set; }

        public Relative[] relatives { get; set; }

        public EnrolleesEducation[] education { get; set; }

        public DocType[] doctypes { get; set; }

        public EducationLevel[] educationlevels { get; set; }

        public EnrolleesStatus enrolleesstatus { get; set; }
    }
}
