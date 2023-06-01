using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSAMS.Models
{
    public class Fields
    {
        [Key]
        [MaxLength(11)]
        public int ID { get; set; }
        [MaxLength(11)]
        [ForeignKey("Form")]
        public int FormID { get; set; }
        public Forms Form { get; set; }
        [MaxLength(64)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }
        [MaxLength(256)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        [MaxLength(1)]
        public int HasComment { get; set; }
        [MaxLength(11)]
        public int Priority { get; set; }
        [MaxLength(11)]
        public int Weight { get; set; }
        public string Choices { get; set; }
        [MaxLength(1)]
        public int Required { get; set; }
    }
}
