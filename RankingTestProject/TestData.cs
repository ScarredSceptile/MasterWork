using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingTestProject
{
    public class TestData
    {
        public static readonly List<TextReview> reviews = new()
        {
            new TextReview() {Id = 1, Review = "One", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 2, Review = "One", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 3, Review = "Two", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 4, Review = "Two", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 5, Review = "Two", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 6, Review = "Two", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 7, Review = "Three", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 8, Review = "Three", Course = 1, Score = 0, Grouping = 1},
        };

        public static readonly RankingInfo[] rankings = new[]
        {
            new RankingInfo() {ID = 1, ReviewID = 1, OtherReviewID = 3, Comparison = "Worse"},
            new RankingInfo() {ID = 2, ReviewID = 3, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 3, ReviewID = 3, OtherReviewID = 4, Comparison = "Equal"},
            new RankingInfo() {ID = 4, ReviewID = 4, OtherReviewID = 3, Comparison = "Equal"},
            new RankingInfo() {ID = 5, ReviewID = 5, OtherReviewID = 6, Comparison = "Equal"},
            new RankingInfo() {ID = 6, ReviewID = 6, OtherReviewID = 5, Comparison = "Equal"},
            new RankingInfo() {ID = 7, ReviewID = 6, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 8, ReviewID = 7, OtherReviewID = 6, Comparison = "Better"},
            new RankingInfo() {ID = 9, ReviewID = 2, OtherReviewID = 1, Comparison = "Equal"},
            new RankingInfo() {ID = 10, ReviewID = 1, OtherReviewID = 2, Comparison = "Equal"},
            new RankingInfo() {ID = 11, ReviewID = 7, OtherReviewID = 8, Comparison = "Equal"},
            new RankingInfo() {ID = 12, ReviewID = 8, OtherReviewID = 7, Comparison = "Equal"},
            new RankingInfo() {ID = 12, ReviewID = 1, OtherReviewID = 4, Comparison = "Worse"},
            new RankingInfo() {ID = 12, ReviewID = 4, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 12, ReviewID = 5, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 12, ReviewID = 7, OtherReviewID = 5, Comparison = "Better"},
        };

        public static readonly List<TextReview> groupReviews = new()
        {
            new TextReview() {Id = 1, Review = "One", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 2, Review = "Two", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 3, Review = "Three", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 4, Review = "Three", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 5, Review = "Three", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 6, Review = "Four", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 7, Review = "Five", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 8, Review = "T", Course = 1, Score = 0, Grouping = 2},
            new TextReview() {Id = 9, Review = "T", Course = 1, Score = 0, Grouping = 2},
            new TextReview() {Id = 10, Review = "T", Course = 1, Score = 0, Grouping = 2},
        };

        public static readonly RankingInfo[] groupRankings = new[]
        {
            new RankingInfo() {ID = 1, ReviewID = 1, OtherReviewID = 2, Comparison = "Worse"},
            new RankingInfo() {ID = 2, ReviewID = 1, OtherReviewID = 3, Comparison = "Worse"},
            new RankingInfo() {ID = 3, ReviewID = 1, OtherReviewID = 4, Comparison = "Worse"},
            new RankingInfo() {ID = 4, ReviewID = 1, OtherReviewID = 5, Comparison = "Worse"},
            new RankingInfo() {ID = 5, ReviewID = 1, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 6, ReviewID = 1, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 7, ReviewID = 2, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 8, ReviewID = 2, OtherReviewID = 3, Comparison = "Worse"},
            new RankingInfo() {ID = 9, ReviewID = 2, OtherReviewID = 4, Comparison = "Worse"},
            new RankingInfo() {ID = 10, ReviewID = 2, OtherReviewID = 5, Comparison = "Worse"},
            new RankingInfo() {ID = 11, ReviewID = 2, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 12, ReviewID = 2, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 13, ReviewID = 3, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 14, ReviewID = 3, OtherReviewID = 2, Comparison = "Better"},
            new RankingInfo() {ID = 15, ReviewID = 3, OtherReviewID = 4, Comparison = "Equal"},
            new RankingInfo() {ID = 16, ReviewID = 3, OtherReviewID = 5, Comparison = "Equal"},
            new RankingInfo() {ID = 17, ReviewID = 3, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 18, ReviewID = 3, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 19, ReviewID = 4, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 20, ReviewID = 4, OtherReviewID = 2, Comparison = "Better"},
            new RankingInfo() {ID = 21, ReviewID = 4, OtherReviewID = 3, Comparison = "Equal"},
            new RankingInfo() {ID = 22, ReviewID = 4, OtherReviewID = 5, Comparison = "Equal"},
            new RankingInfo() {ID = 23, ReviewID = 4, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 24, ReviewID = 4, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 25, ReviewID = 5, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 26, ReviewID = 5, OtherReviewID = 2, Comparison = "Better"},
            new RankingInfo() {ID = 27, ReviewID = 5, OtherReviewID = 3, Comparison = "Equal"},
            new RankingInfo() {ID = 28, ReviewID = 5, OtherReviewID = 4, Comparison = "Equal"},
            new RankingInfo() {ID = 29, ReviewID = 5, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 30, ReviewID = 5, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 31, ReviewID = 6, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 32, ReviewID = 6, OtherReviewID = 2, Comparison = "Better"},
            new RankingInfo() {ID = 33, ReviewID = 6, OtherReviewID = 3, Comparison = "Better"},
            new RankingInfo() {ID = 34, ReviewID = 6, OtherReviewID = 4, Comparison = "Better"},
            new RankingInfo() {ID = 35, ReviewID = 6, OtherReviewID = 5, Comparison = "Better"},
            new RankingInfo() {ID = 36, ReviewID = 6, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 37, ReviewID = 7, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 38, ReviewID = 7, OtherReviewID = 2, Comparison = "Better"},
            new RankingInfo() {ID = 39, ReviewID = 7, OtherReviewID = 3, Comparison = "Better"},
            new RankingInfo() {ID = 40, ReviewID = 7, OtherReviewID = 4, Comparison = "Better"},
            new RankingInfo() {ID = 41, ReviewID = 7, OtherReviewID = 5, Comparison = "Better"},
            new RankingInfo() {ID = 42, ReviewID = 7, OtherReviewID = 6, Comparison = "Better"},
            new RankingInfo() {ID = 43, ReviewID = 6, OtherReviewID = 8, Comparison = "Better"},
            new RankingInfo() {ID = 44, ReviewID = 6, OtherReviewID = 9, Comparison = "Better"},
            new RankingInfo() {ID = 45, ReviewID = 6, OtherReviewID = 10, Comparison = "Better"},
            new RankingInfo() {ID = 46, ReviewID = 1, OtherReviewID = 9, Comparison = "Better"},
            new RankingInfo() {ID = 47, ReviewID = 1, OtherReviewID = 10, Comparison = "Better"},
        };

        public static TextReview[] SigReviews = new[]
        {
            new TextReview() {Id = 1, Review = "T", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 2, Review = "T", Course = 1, Score = 0, Grouping = 2},
            new TextReview() {Id = 3, Review = "T", Course = 1, Score = 0, Grouping = 3},
            new TextReview() {Id = 4, Review = "T", Course = 1, Score = 0, Grouping = 4},
            new TextReview() {Id = 5, Review = "T", Course = 1, Score = 0, Grouping = 5},
            new TextReview() {Id = 6, Review = "T", Course = 1, Score = 0, Grouping = 6},
            new TextReview() {Id = 7, Review = "T", Course = 1, Score = 0, Grouping = 7},
        };

        public static TextReview[] Extra = new[]
        {
            new TextReview() {Id = 8, Review = "T", Course = 1, Score = 0, Grouping = 1},
            new TextReview() {Id = 9, Review = "T", Course = 1, Score = 0, Grouping = 2},
        };

        public static List<int> SigIDs = new() { 1, 2, 3, 4, 5, 6, 7 };

        public static RankingInfo[] SigIDRanks = new[]
        {
            new RankingInfo() {ID = 1, ReviewID = 1, OtherReviewID =2, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 2, OtherReviewID =1, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 3, OtherReviewID =4, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 4, OtherReviewID =3, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 5, OtherReviewID =6, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 6, OtherReviewID =5, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 1, OtherReviewID =7, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 7, OtherReviewID =1, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 2, OtherReviewID =5, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 5, OtherReviewID =2, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 5, OtherReviewID =7, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 7, OtherReviewID =5, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 8, OtherReviewID =9, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 9, OtherReviewID =8, Comparison = "N/A"},
            new RankingInfo() {ID = 1, ReviewID = 1, OtherReviewID =9, Comparison = "N/A"},
        };

        public static bool EqualRankingLists(RankingInfo[] expected, RankingInfo[] actual)
        {
            if (expected.Length != actual.Length) return false;

            foreach (var r in expected)
            {
                if (actual.All(a => a.IsEqual(r) == false))
                    return false;
            }
            return true;
        }
    }
}
