using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationWithAuth.Models
{
    public class DocType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanBeDeleted { set; get; }
    }
}
