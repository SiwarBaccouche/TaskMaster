using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Areas.Identity.Data;

namespace TaskMaster.Models
{
    public class Team
    {
        [Key]
        [Column("TeamId")]
        public int TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string TeamName { get; set; }

        public string Description { get; set; }

        // Foreign key for Team Lead (User)
        [ForeignKey("TeamLead")]
        public string TeamLeadId { get; set; } 
        public TaskMasterUser TeamLead { get; set; }

        public Team() { }
    }
}
