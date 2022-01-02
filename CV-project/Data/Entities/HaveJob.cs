using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("HaveJob")]
    public partial class HaveJob
    {
        [Key]
        [Column("CompanyID")]
        public Guid CompanyId { get; set; }
        [Key]
        [Column("JobID")]
        public Guid JobId { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("HaveJobs")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(JobId))]
        [InverseProperty("HaveJobs")]
        public virtual Job Job { get; set; }
    }
}
