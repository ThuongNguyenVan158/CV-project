using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("WebCV")]
    public partial class WebCv
    {
        public WebCv()
        {
            Applicants = new HashSet<Applicant>();
        }

        [Key]
        [Column("CVID")]
        public Guid Cvid { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DoB { get; set; }
        [StringLength(100)]
        public string Major { get; set; }
        [StringLength(200)]
        public string University { get; set; }
        [StringLength(1000)]
        public string Skills { get; set; }
        [StringLength(1000)]
        public string BriefIntroduction { get; set; }
        [StringLength(1000)]
        public string CareerObjectives { get; set; }
        [StringLength(1000)]
        public string Awards { get; set; }
        [StringLength(1000)]
        public string Certificates { get; set; }
        public short? WorkExperience { get; set; }
        [StringLength(1000)]
        public string Activities { get; set; }

        [InverseProperty(nameof(Applicant.Cv))]
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
