using CSAMS.APIModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Assignments : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Timestamp]
        public byte[] Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime PublishedDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Deadline { get; set; }
        [MaxLength(11)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Courses Course { get; set; }
        [MaxLength(11)]
        [ForeignKey("Submission")]
        public int SubmissionID { get; set; }
        public Submissions Submission { get; set; }
        [MaxLength(1)]
        public byte ReviewEnabled { get; set; }
        [MaxLength(11)]
        [ForeignKey("Review")]
        public int ReviewID { get; set; }
        public Reviews Review { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ReviewDeadline { get; set; }
        [MaxLength(11)]
        public int Reviewers { get; set; }


        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Assignments;
            return (otherModel.ID == ID &&
                otherModel.Name == Name &&
                otherModel.Description == Description &&
                otherModel.Created == Created &&
                otherModel.PublishedDate == PublishedDate &&
                otherModel.Deadline == Deadline &&
                otherModel.CourseID == CourseID &&
                otherModel.SubmissionID == SubmissionID &&
                otherModel.ReviewEnabled == ReviewEnabled &&
                otherModel.ReviewID == ReviewID &&
                otherModel.ReviewDeadline == ReviewDeadline &&
                otherModel.Reviewers == Reviewers);
        }
    }
}
