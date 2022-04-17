using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EnrolleesCompetition
    {
        public int Id { get; set; }
        public int EnrolleeID { get; set; }
        public int CompetitionId { get; set; }

    }
}
