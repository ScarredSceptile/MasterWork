using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewRanking.Models
{
    public class CourseReview
    {
        public int ID { get; set; }
        public int Course { get; set; }
        public int Reviews { get; set; }
    }
}
