using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("HaveEvent")]
    public partial class HaveEvent
    {
        [Key]
        [Column("CompanyID")]
        public Guid CompanyId { get; set; }
        [Key]
        [Column("EventID")]
        public Guid EventId { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Venue { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EventStartDate { get; set; }
        [StringLength(500)]
        public string EventLink { get; set; }
        [Column(TypeName = "time(5)")]
        public TimeSpan? EventEndTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EventEndDate { get; set; }
        [Column(TypeName = "time(5)")]
        public TimeSpan? EventStartTime { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("HaveEvents")]
        public virtual Company Company { get; set; }
        [ForeignKey(nameof(EventId))]
        [InverseProperty("HaveEvents")]
        public virtual Event Event { get; set; }
    }
}
