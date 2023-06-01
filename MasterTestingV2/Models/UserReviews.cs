using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class UserReviews : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("Reviewer")]
        public int UserReviewer { get; set; }
        public Users Reviewer { get; set; }
        [MaxLength(11)]
        [ForeignKey("Target")]
        public int UserTarget { get; set; }
        public Users Target { get; set; }
        [MaxLength(11)]
        [ForeignKey("Review")]
        public int ReviewID { get; set; }
        public Reviews Review { get; set; }
        [MaxLength(11)]
        [ForeignKey("Assignment")]
        public int AssignmentID { get; set; }
        public Assignments Assignment { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string? Answer { get; set; }
        public string? Comment { get; set; }
        public string? Language { get; set; }


        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as UserReviews;
            return (otherModel.ID == ID &&
                otherModel.UserReviewer == UserReviewer &&
                otherModel.UserTarget == UserTarget &&
                otherModel.ReviewID == ReviewID &&
                otherModel.AssignmentID == AssignmentID &&
                otherModel.Assignment.AssertEqual(Assignment) &&
                otherModel.Type == Type &&
                otherModel.Name == Name &&
                otherModel.Label == Label &&
                otherModel.Answer == Answer &&
                otherModel.Comment == Comment);
        }
    }
}
