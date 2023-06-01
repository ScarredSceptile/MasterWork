using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class UserCourses
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public Users User { get; set; }
        [MaxLength(11)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Courses Course { get; set; }
    }
}
