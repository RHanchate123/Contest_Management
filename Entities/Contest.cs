using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contest_Management.Entities
{
    public class Contest : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AccessLevel accessLevel { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string Prize { get; set; }
        public string? UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

    }

    public enum AccessLevel
    {
        VIP = 0,
        Normal = 1,
        SignedIn = 2, 
        Guest = 4
    }
}
