using CV_project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class PagingJobCompany
    {
        public PagingList<Company> company { get; set;}
        public List<JobPerCompany> listJobPerCompany { get; set; }
    }
}
