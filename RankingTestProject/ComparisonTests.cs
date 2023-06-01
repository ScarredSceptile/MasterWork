using ReviewRanking.RankingResources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingTestProject
{
    [TestClass]
    public class ComparisonTests
    {
        [TestMethod]
        public void TestSignificantIDs()
        {
            var group = TestData.groupReviews.GroupBy(n => n.Grouping).First();
            var test = RankingProcess.GetSignificantIDs(group, TestData.groupRankings);
            foreach (var item in test)
                Console.WriteLine(item);
            Assert.IsNotNull(test);
            Assert.AreEqual(3, test.Count);
            Assert.IsTrue(test.Contains(1));
            Assert.IsTrue(test.Contains(7));
            Assert.IsTrue(test.Contains(3) || test.Contains(4) || test.Contains(5));
        }

        [TestMethod]
        public void TestSignificantReviews()
        {
            var fullData = RankingProcess.AddOldComparisons(TestData.groupReviews.ToArray(), TestData.groupRankings.ToList());
            var group = fullData.GroupBy(n => n.Grouping).First();
            var test = RankingProcess.GetSignificantReviews(group);
            foreach (var item in test)
                Console.WriteLine(item);
            Assert.IsNotNull(test);
            Assert.AreEqual(3, test.Count);
            Assert.IsTrue(test.Any(c => c.Id == 1));
            Assert.IsTrue(test.Any(c => c.Id == 7));
            Assert.IsTrue(test.Any(c => c.Id == 3 || c.Id == 4 || c.Id == 5));

        }

        (int ID, int Count)[] ExpectedIDCount = new[]
        {
            (1, 2),
            (2, 2),
            (3, 1),
            (4, 1),
            (5, 3),
            (6, 1),
            (7, 2),
        };

        [TestMethod]
        public void TestSignificantReviewComplete()
        {
            var rev = TestData.SigReviews.ToArray();
            rev[0].BetterReviews.Add(rev[1].Id);
            rev[0].EqualReviews.Add(rev[6].Id);
            rev[0].WorseReviews.Add(TestData.Extra[1].Id);
            rev[1].WorseReviews.Add(rev[0].Id);
            rev[1].WorseReviews.Add(rev[4].Id);
            rev[2].WorseReviews.Add(rev[3].Id);
            rev[3].WorseReviews.Add(rev[2].Id);
            rev[4].WorseReviews.Add(rev[5].Id);
            rev[4].WorseReviews.Add(rev[1].Id);
            rev[4].WorseReviews.Add(rev[6].Id);
            rev[5].WorseReviews.Add(rev[4].Id);
            rev[6].WorseReviews.Add(rev[0].Id);
            rev[6].WorseReviews.Add(rev[4].Id);
            var test = RankingProcess.CheckSignificantReviewComplete(rev.ToList());
            Assert.IsNotNull(test);
            Assert.IsTrue(ExpectedIDCount.Length == test.Length);
            Assert.IsTrue(test.Where(n => n.review.Id == 1 && n.Count == 2).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 2 && n.Count == 2).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 3 && n.Count == 1).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 4 && n.Count == 1).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 5 && n.Count == 3).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 6 && n.Count == 1).Any());
            Assert.IsTrue(test.Where(n => n.review.Id == 7 && n.Count == 2).Any());
        }

        [TestMethod]
        public void TestSignificantIDComplete()
        {
            var test = RankingProcess.CheckSignificantIDComplete(TestData.SigIDs, TestData.SigIDRanks);
            Assert.IsNotNull(test);
            Assert.IsTrue(ExpectedIDCount.Length == test.Length);
            Assert.IsTrue(ExpectedIDCount.All(n => test.Any(c => c.ID == n.ID && c.Count == n.Count)));
        }

        RankingInfo[] ExpectedDifferent = new[]
        {
            new RankingInfo() {ID = 1, ReviewID = 3, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 2, ReviewID = 7, OtherReviewID = 3, Comparison = "Better"},
            new RankingInfo() {ID = 3, ReviewID = 4, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 4, ReviewID = 7, OtherReviewID = 4, Comparison = "Better"},
            new RankingInfo() {ID = 5, ReviewID = 3, OtherReviewID = 8, Comparison = "Worse"},
            new RankingInfo() {ID = 6, ReviewID = 8, OtherReviewID = 3, Comparison = "Better"},
            new RankingInfo() {ID = 7, ReviewID = 4, OtherReviewID = 8, Comparison = "Worse"},
            new RankingInfo() {ID = 8, ReviewID = 8, OtherReviewID = 4, Comparison = "Better"},
            new RankingInfo() {ID = 9, ReviewID = 1, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 10, ReviewID = 7, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 11, ReviewID = 1, OtherReviewID = 8, Comparison = "Worse"},
            new RankingInfo() {ID = 12, ReviewID = 8, OtherReviewID = 1, Comparison = "Better"},
        };

        [TestMethod]
        public void TestWorseComparison()
        {
            var test = RankingProcess.DifferentComparison(3, 7, Values.Worse, TestData.rankings);
            Console.WriteLine(ExpectedDifferent.Length);
            Console.WriteLine(test.Count);
            foreach (var item in test)
                Console.WriteLine($"ReviewID: {item.ReviewID}, OtherReviewID: {item.OtherReviewID}, Comparison: {item.Comparison}");
            Assert.IsTrue(TestData.EqualRankingLists(ExpectedDifferent, test.ToArray()));
        }

        [TestMethod]
        public void TestBetterComparison()
        {
            var test = RankingProcess.DifferentComparison(7, 3, Values.Better, TestData.rankings);
            Console.WriteLine(ExpectedDifferent.Length);
            Console.WriteLine(test.Count);
            foreach (var item in test)
                Console.WriteLine($"ReviewID: {item.ReviewID}, OtherReviewID: {item.OtherReviewID}, Comparison: {item.Comparison}");
            Assert.IsTrue(TestData.EqualRankingLists(ExpectedDifferent, test.ToArray()));
        }

        [TestMethod]
        public void TestNewDifferentComparison()
        {
            var reviews = RankingProcess.AddOldComparisons(TestData.reviews.ToArray(), TestData.rankings.ToList());
            var test = RankingProcess.NewDifferentComparison(reviews.Where(c => c.Id == 7).First(), reviews.Where(c => c.Id == 3).First(), reviews);
            Console.WriteLine(ExpectedDifferent.Length);
            Console.WriteLine(test.Count);
            foreach (var item in test)
                Console.WriteLine($"ReviewID: {item.ReviewID}, OtherReviewID: {item.OtherReviewID}, Comparison: {item.Comparison}");
            Assert.IsTrue(TestData.EqualRankingLists(ExpectedDifferent, test.ToArray()));
        }

        RankingInfo[] ExpectedEqual = new[]
        {
            new RankingInfo() {ID = 1, ReviewID = 3, OtherReviewID = 6, Comparison = "Equal"},
            new RankingInfo() {ID = 2, ReviewID = 6, OtherReviewID = 3, Comparison = "Equal"},
            new RankingInfo() {ID = 3, ReviewID = 6, OtherReviewID = 4, Comparison = "Equal"},
            new RankingInfo() {ID = 4, ReviewID = 4, OtherReviewID = 6, Comparison = "Equal"},
            new RankingInfo() {ID = 5, ReviewID = 5, OtherReviewID = 4, Comparison = "Equal"},
            new RankingInfo() {ID = 6, ReviewID = 4, OtherReviewID = 5, Comparison = "Equal"},
            new RankingInfo() {ID = 7, ReviewID = 3, OtherReviewID = 5, Comparison = "Equal"},
            new RankingInfo() {ID = 8, ReviewID = 5, OtherReviewID = 3, Comparison = "Equal"},
            new RankingInfo() {ID = 9, ReviewID = 5, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 10, ReviewID = 1, OtherReviewID = 5, Comparison = "Worse"},
            new RankingInfo() {ID = 11, ReviewID = 6, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 12, ReviewID = 1, OtherReviewID = 6, Comparison = "Worse"},
            new RankingInfo() {ID = 13, ReviewID = 4, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 14, ReviewID = 7, OtherReviewID = 4, Comparison = "Better"},
            new RankingInfo() {ID = 15, ReviewID = 1, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 16, ReviewID = 7, OtherReviewID = 1, Comparison = "Better"},
            new RankingInfo() {ID = 17, ReviewID = 3, OtherReviewID = 7, Comparison = "Worse"},
            new RankingInfo() {ID = 18, ReviewID = 7, OtherReviewID = 3, Comparison = "Better"},
        };

        [TestMethod]
        public void TestEqualComparison()
        {
            var test = RankingProcess.EqualComparison(3, 6, TestData.rankings);
            Console.WriteLine(ExpectedEqual.Length);
            Console.WriteLine(test.Count);
            foreach (var item in test)
                Console.WriteLine($"ReviewID: {item.ReviewID}, OtherReviewID: {item.OtherReviewID}, Comparison: {item.Comparison}");
            Assert.IsTrue(TestData.EqualRankingLists(ExpectedEqual, test.ToArray()));
        }

        [TestMethod]
        public void TestNewEqualComparison()
        {
            // Even as the reviews is readonly, the data changes
            TestData.reviews.ForEach(c => { c.BetterReviews.Clear(); c.WorseReviews.Clear(); c.EqualReviews.Clear(); });

            var reviews = RankingProcess.AddOldComparisons(TestData.reviews.ToArray(), TestData.rankings.ToList());
            var test = RankingProcess.NewEqualComparison(reviews.Where(c => c.Id == 6).First(), reviews.Where(c => c.Id == 3).First(), reviews);
            Console.WriteLine(ExpectedEqual.Length);
            Console.WriteLine(test.Count);
            foreach (var item in test)
                Console.WriteLine($"ReviewID: {item.ReviewID}, OtherReviewID: {item.OtherReviewID}, Comparison: {item.Comparison}");
            Assert.IsTrue(TestData.EqualRankingLists(ExpectedEqual, test.ToArray()));
        }
    }
}
