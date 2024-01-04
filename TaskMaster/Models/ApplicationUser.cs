using Microsoft.AspNetCore.Identity;


    namespace TaskMaster.Models
    {
        public class AspNetUsers : IdentityUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool EmailConfirmed { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public string ConcurrencyStamp { get; set; }
            public string PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public DateTimeOffset? LockoutEnd { get; set; }
            public bool LockoutEnabled { get; set; }
            public int AccessFailedCount { get; set; }
        }
    }

