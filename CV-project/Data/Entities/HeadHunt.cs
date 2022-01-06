using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Data.Entities
{
    [Table("HeadHunt")]
    public class HeadHunt
    {
        [Key]
        [Column("HeadHuntId")]
        public Guid HeadHuntId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Message { get; set; }
        //[Column("AccountID")]
        //public Guid AccountId { get; set; }

        //[ForeignKey(nameof(AccountId))]
        //[InverseProperty("HeadHunts")]
        //public virtual Account Account { get; set; }
    }
}
