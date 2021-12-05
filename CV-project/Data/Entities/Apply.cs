using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Apply")]
    public partial class Apply
    {
        [Key]
        [Column("ApplicantID")]
        public int ApplicantId { get; set; }
        [Key]
        [Column("CompanyID")]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(ApplicantId))]
        [InverseProperty("Applies")]
        public virtual Applicant Applicant { get; set; }
        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Applies")]
        public virtual Company Company { get; set; }
    }
}
