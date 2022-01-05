using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CV_project.Data.Entities
{
    [Table("Event")]
    public partial class Event
    {
        public Event()
        {
            HaveEvents = new HashSet<HaveEvent>();
        }

        [Key]
        [Column("EventID")]
        public Guid EventId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(HaveEvent.Event))]
        public virtual ICollection<HaveEvent> HaveEvents { get; set; }
    }
}
