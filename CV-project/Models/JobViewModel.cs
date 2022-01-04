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
    }
}
