using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Forms : IAPIModel
    {
        [Key]
        [MaxLength]
        public int ID { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "VARCHAR")]
        public string Prefix { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Timestamp]
        public byte[] Created { get; set; }

        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Forms;
            return (otherModel.ID == ID &&
                otherModel.Prefix == Prefix &&
                otherModel.Name == Name &&
                otherModel.Created == Created);
        }
    }
}
