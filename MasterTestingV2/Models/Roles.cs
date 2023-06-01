using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Roles : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "VARCHAR")]
        public string Key { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }

        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Roles;
            return (otherModel.ID == ID &&
                otherModel.Name == Name &&
                otherModel.Key == Key);
        }
    }
}
