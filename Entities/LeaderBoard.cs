using IdentityApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contest_Management.Entities
{
    public class LeaderBoard : BaseEntity
    {
        public string UserID { get; set; }

        public decimal Score { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}
