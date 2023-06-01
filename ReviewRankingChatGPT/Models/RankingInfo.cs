using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewRanking.Models
{
    public class RankingInfo
    {
        public int ID { get; set; }
        public int ReviewID { get; set; }
        public int OtherReviewID { get; set; }
        public string Comparison { get; set; }

        public bool IsEqual(RankingInfo other)
        {
            return ReviewID == other.ReviewID &&
                OtherReviewID == other.OtherReviewID &&
                Comparison == other.Comparison;
        }
    }
}
