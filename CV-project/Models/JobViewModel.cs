using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class JobViewModel
    {
        public Guid JobId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Information { get; set; }
        public DateTime Deadline { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Requirement { get; set; }
        public string Language { get; set; }
        public string Worktime { get; set; }
        public int NumEmployee { get; set; }
        public string SalaryPerMonth { get; set; }
    }
}
