using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class CompetitionsExam
    {
        public int Id { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public int CompetitionId { get; set; }
        public bool egeIsFeasible  { get; set; }
        public bool examIsFeasible { get; set; }
        public bool achievementIsFeasible { get; set; }

    }
}