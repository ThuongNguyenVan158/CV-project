using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Applicant")]
    public partial class Applicant
    {
        public Applicant()
        {
            Applies = new HashSet<Apply>();
        }

        [Key]
        [Column("ApplicantID")]
        public int ApplicantId { get; set; }
        [Required]
        [StringLength(30)]
        public string Account { get; set; }
        [Required]
        [StringLength(500)]
        public string Password { get; set; }
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
        [Required]
        [Column("UploadCV")]
        public string UploadCv { get; set; }
        [Column("CVID")]
        public int? Cvid { get; set; }

        [ForeignKey(nameof(Cvid))]
        [InverseProperty(nameof(WebCv.Applicants))]
        public virtual WebCv Cv { get; set; }
        [InverseProperty(nameof(Apply.Applicant))]
        public virtual ICollection<Apply> Applies { get; set; }
    }
}
