using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskaholic.Models
{
    public class Assignment
    {
        public string Title { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsComplete { get; set; }
        public List<SubAssignment>SubAssignments { get; set; }
    }
}
