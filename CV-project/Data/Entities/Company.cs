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
        }

        [Key]
        [Column("CompanyID")]
        public int CompanyId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public short? NoEmployee { get; set; }
        [Required]
        [StringLength(30)]
        public string Category { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [InverseProperty(nameof(Apply.Company))]
        public virtual ICollection<Apply> Applies { get; set; }
    }
}
