using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Models
{
    public class RegisterCompanyViewModel
    {
        [Required()]
        public string UserName { get; set; }
        [Required()]
        public string Password { get; set; }
        [Required()]
        //public string RePassword { get; set; }
        //[Required()]
        public string FullName { get; set; }
    }
}
