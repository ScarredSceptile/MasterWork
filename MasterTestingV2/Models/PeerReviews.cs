using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class PeerReviews
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("Assignment")]
        public int AssignmentID { get; set; }
        public Assignments Assignment { get; set; }
        [MaxLength(11)]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public Users User { get; set; }
        [MaxLength(11)]
        [ForeignKey("ReviewUser")]
        public int ReviewUserID { get; set; }
        public Users ReviewUser { get; set; }
        public string Words { get; set; }
        public int Group {  get; set; }
    }
}
