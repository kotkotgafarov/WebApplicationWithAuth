using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithAuth.Models
{
    public class Application
    {
        public Enrollee enrollee { get; set; }
        public Competition[] competitions { get; set; }
        public ApplicationsExam[] applicationsexam { get; set; }
        public SubjectSubstitutor[] subjectsubstitutors { get; set; }
        public EnrolleesDoc[] docs { get; set; }
        public ApplicationsAchievement[] applicationsachievement { get; set; }
        public ApplicationsContract[] applicationscontract { get; set; }
        public EnrolleesStatus enrolleesstatus { get; set; }
    }
}
