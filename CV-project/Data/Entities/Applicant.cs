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
        public Guid ApplicantId { get; set; }
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
        public short? WorkExperiences { get; set; }
        [StringLength(1000)]
        public string Activities { get; set; }
        [Column("CVUpload")]
        public string Cvupload { get; set; }
        public string Contact { get; set; }
        [Column("CVID")]
        public Guid Cvid { get; set; }
        [Column("AccountID")]
        public Guid AccountId { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Applicants")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(Cvid))]
        [InverseProperty(nameof(WebCv.Applicants))]
        public virtual WebCv Cv { get; set; }
        [InverseProperty(nameof(Apply.Applicant))]
        public virtual ICollection<Apply> Applies { get; set; }
    }
}
