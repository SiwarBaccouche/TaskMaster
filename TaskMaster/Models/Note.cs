using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskMaster.Areas.Identity.Data;

namespace TaskMaster.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string? Text { get; set; }

        [ForeignKey("Writer")]
        public string? WriterId { get; set; }
        public TaskMasterUser? Writer { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? ImportanceLevel { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
