using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class CompanyViewModel
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public short? NoEmployee { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

    }
}
