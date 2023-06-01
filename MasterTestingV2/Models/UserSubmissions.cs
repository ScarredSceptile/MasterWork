using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class UserSubmissions
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public Users User { get; set; }
        [MaxLength(11)]
        [ForeignKey("Assignment")]
        public int AssignmentID { get; set; }
        public Assignments Assignment { get; set; }
        [MaxLength(11)]
        [ForeignKey("Submission")]
        public int SubmissionID { get; set; }
        public Submissions Submission { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Answer { get; set; }
        public string Comment { get; set; }
    }
}
