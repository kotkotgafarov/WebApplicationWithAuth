using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EnrolleesDoc
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuedBy { get; set; }
        public string Authority { get; set; }
        public int EnrolleeID { get; set; }
        public int DocTypeId { get; set; }

        [NotMapped]
        public bool Changed { set; get; }
        [NotMapped]
        public string FileName { set; get; }
        [NotMapped]
        public int FileId { set; get; }
        [NotMapped]
        public bool CanBeDeleted { set; get; }

    }
}
