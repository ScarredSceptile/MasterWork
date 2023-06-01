using CSAMS.APIModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Reviews : IAPIModel
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("Form")]
        public int FormID { get; set; }
        public Forms Form { get; set; }

        public bool AssertEqual(IAPIModel other)
        {
            var otherModel = other as Reviews;
            return (otherModel.ID == ID &&
                otherModel.FormID == FormID &&
                otherModel.Form.AssertEqual(Form));
        }
    }
}
