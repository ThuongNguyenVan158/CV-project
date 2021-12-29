using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class InfoViewModel
    {
        public Guid accountId { get; set; }
        public string FullName { get; set; }
        public int AccountType { get; set; }
    }
}
