using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class EnrolleesFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int EnrolleeID { get; set; }
        public int EnrolleesDocID { get; set; }
    }
}
