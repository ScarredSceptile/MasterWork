using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterRankingAPI.Models
{
    public class TextReview
    {
        [Key] public int Id { get; set; }
        public string Review { get; set; }
        public int Course { get; set; }
        public int Score { get; set; }
        public int Grouping { get; set; }
        [NotMapped]
        public List<int> BetterReviews = new();
        [NotMapped]
        public List<int> EqualReviews = new();
        [NotMapped]
        public List<int> WorseReviews = new();
    }
}
