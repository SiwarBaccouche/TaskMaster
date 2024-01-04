using Microsoft.AspNetCore.Identity;
using TaskMaster.Models;

namespace TaskMaster.Areas.Identity.Data
{
    public class TaskMasterUser : IdentityUser
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
       
    } 
}
