using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithAuth.Models
{
    public class LKAUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isModerator { get; set; }

    }
}
