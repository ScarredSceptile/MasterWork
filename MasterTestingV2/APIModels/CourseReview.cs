using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTestingV2.APIModels
{
    public  class CourseReview
    {
        public int ID { get; set; }
        public int Course { get; set; }
        public int Reviews { get; set; }
    }
}
