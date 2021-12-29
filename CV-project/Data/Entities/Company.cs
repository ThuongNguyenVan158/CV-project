using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Company")]
    public partial class Company
    {
        public Company()
        {
            Applies = new HashSet<Apply>();
            HaveEvents = new HashSet<HaveEvent>();
            HaveJobs = new HashSet<HaveJob>();
        }

        [Key]
        [Column("CompanyID")]
        public Guid CompanyId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public short? NoEmployee { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column("AccountID")]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Companies")]
        public virtual Account Account { get; set; }
        [InverseProperty(nameof(Apply.Company))]
        public virtual ICollection<Apply> Applies { get; set; }
        [InverseProperty(nameof(HaveEvent.Company))]
        public virtual ICollection<HaveEvent> HaveEvents { get; set; }
        [InverseProperty(nameof(HaveJob.Company))]
        public virtual ICollection<HaveJob> HaveJobs { get; set; }
    }
}
