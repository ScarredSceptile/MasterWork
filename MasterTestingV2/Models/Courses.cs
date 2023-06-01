using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public enum SemesterSeasons
    {
        Fall,
        Spring
    }

    public class Courses : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string Hash { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string CourseCode { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string CourseName { get; set; }
        [MaxLength(11)]
        [ForeignKey("User")]
        public int Teacher { get; set; }
        public Users User { get; set; }
        public string Description { get; set; }
        [MaxLength(11)]
        public int Year { get; set; }
        public SemesterSeasons Semester { get; set; }
        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Courses;
            return (otherModel.ID == ID &&
                otherModel.Hash == Hash &&
                otherModel.CourseCode == CourseCode &&
                otherModel.CourseName == CourseName &&
                otherModel.Teacher == Teacher &&
                otherModel.User.AssertEqual(User) &&
                otherModel.Description == Description &&
                otherModel.Year == Year &&
                otherModel.Semester == Semester);
        }
    }
}
