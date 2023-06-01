using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Users : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("UserRole")]
        public int Role { get; set; }
        public Roles UserRole { get; set; }

        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Users;
            return (otherModel.ID == ID &&
                otherModel.Role == Role &&
                otherModel.UserRole.AssertEqual(UserRole));
        }
    }
}
