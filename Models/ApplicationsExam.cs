using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class ApplicationsExam
    {
        public int Id { get; set; }
        public int CompetitionsExamId { get; set; }
        public int EnrolleeId { get; set; }
        public int Type { get; set; } // 1 = ege, 2 = exam, 3 = achievement
        public int SubjectId { get; set; }
        public int SubjectsSubstitutorId { get; set; }  
        public int Score { get; set; }
        public int EnrolleesDocId { get; set; }

        [NotMapped]
        public string SubjectName { get; set; }

        [NotMapped]
        public string SubjectsSubstitutorName { get; set; }

        [NotMapped]
        public bool egeIsFeasible { get; set; }

        [NotMapped]
        public bool examIsFeasible { get; set; }

        [NotMapped]
        public bool achievementIsFeasible { get; set; }

        [NotMapped]
        public int MinScore { get; set; }

        [NotMapped]
        public int MaxScore { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string EnrolleesDocName { get; set; }
    }
}
