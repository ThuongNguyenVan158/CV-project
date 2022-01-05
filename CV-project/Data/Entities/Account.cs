using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            Applicants = new HashSet<Applicant>();
            Companies = new HashSet<Company>();
            HeadHunts = new HashSet<HeadHunt>();
        }

        [Key]
        [Column("AccountID")]
        public Guid AccountId { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public int AccountType { get; set; }

        [InverseProperty(nameof(Applicant.Account))]
        public virtual ICollection<Applicant> Applicants { get; set; }
        [InverseProperty(nameof(Company.Account))]
        public virtual ICollection<Company> Companies { get; set; }
        [InverseProperty(nameof(HeadHunt.Account))]
        public virtual ICollection<HeadHunt> HeadHunts { get; set; }
    }
}
