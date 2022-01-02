using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Job")]
    public partial class Job
    {
        public Job()
        {
            HaveJobs = new HashSet<HaveJob>();
        }

        [Key]
        [Column("JobID")]
        public Guid JobId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Vacancy { get; set; }

        [InverseProperty(nameof(HaveJob.Job))]
        public virtual ICollection<HaveJob> HaveJobs { get; set; }
    }
}
