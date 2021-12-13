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
        public int Cvid { get; set; }
        [Required]
        [StringLength(30)]
        public string FullName { get; set; }
        [Column(TypeName = "date")]
        public DateTime DoB { get; set; }
        [Required]
        [StringLength(30)]
        public string Major { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public short Experience { get; set; }

        [InverseProperty(nameof(Applicant.Cv))]
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
