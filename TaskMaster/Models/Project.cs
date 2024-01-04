using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

       
        public string TeamLead { get; set; }
        public Project() { }

    }
} 
