using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class JobPerCompany
    {
        public string Name { get; set; }
        public string Vacancy { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
    }
}
