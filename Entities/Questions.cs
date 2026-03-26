using System.ComponentModel.DataAnnotations.Schema;

namespace Contest_Management.Entities
{
    public class Questions : BaseEntity
    {
        public string Title { get; set; }
        public string Answer { get; set; }

        public int ContestID { get; set; }

        [ForeignKey("ContestID")]
        public  Contest Contest { get; set; }

    }
}
