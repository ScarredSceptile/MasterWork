using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewRanking.Models
{
    public class TextReview
    {
        [Key] public int Id { get; set; }
        public string Review { get; set; }
        public int Course { get; set; }
        public int Score { get; set; }
        public int Grouping { get; set; }

        [NotMapped]
        public HashSet<int> BetterReviews = new();
        [NotMapped]
        public HashSet<int> EqualReviews = new();
        [NotMapped]
        public HashSet<int> WorseReviews = new();

        public int Comparisons()
        {
            return BetterReviews.Count + EqualReviews.Count + WorseReviews.Count;
        }

        public bool IsCompared(int t)
        {
            return BetterReviews.Contains(t) || EqualReviews.Contains(t) || WorseReviews.Contains(t);
        }
    }
}
