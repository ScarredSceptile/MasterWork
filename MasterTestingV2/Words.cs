using CSAMS.Models;
using StopWord;

namespace MasterTestingV2
{
    internal class Words
    {
        /// <summary>
        /// Returns a list of all english words
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<string> Allwords(AppDbContext db)
        {
            List<string> listEn = new List<string>();
            List<string> listNo = new List<string>();
            var words = db.UserReviews.Where(r => r.Comment != null).Select(r => new { Review = r.Comment, Lang = r.Language }).ToList();
            words.AddRange(db.UserReviews.Where(r => r.Type == "textarea" && r.Answer != null).Select(r => new { Review = r.Answer, Lang = r.Language }).ToList());
            foreach (var word in words)
            {
                var txt = word.Review.RemoveStopWords(word.Lang);
                if (word.Lang == "en")
                    listEn.Add(txt);
                else
                    listNo.Add(txt);
            }
            var allwordsEn = Utility.GetWordAmount(listEn.ToArray());
            var allwordsNo = Utility.GetWordAmount(listNo.ToArray());
            allwordsEn = allwordsEn.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(x => x.Key, x => x.Value);
            allwordsNo = allwordsNo.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(x => x.Key, x => x.Value);

            //Utility.WriteResult("allWordsEn", allwordsEn);
            //Utility.WriteResult("allWordsNo", allwordsNo);
            return allwordsEn.Select(n => n.Key).ToList();
        }

        /// <summary>
        /// Returns a dictionary of words based on how many review fields a words appears in
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Dictionary<string, int> WordsByReview(AppDbContext db)
        {
            List<string> commonReviewList = new List<string>();
            var words = db.UserReviews.Where(r => r.Comment != null).Select(r => new { Review = r.Comment, Lang = r.Language }).ToList();
            words.AddRange(db.UserReviews.Where(r => r.Type == "textarea" && r.Answer != null).Select(r => new { Review = r.Answer, Lang = r.Language }).ToList());
            foreach (var word in words)
            {
                var txt = word.Review.RemoveStopWords(word.Lang);
                if (word.Lang == "en")
                    commonReviewList.AddRange(Utility.GetWordAmount(new[] { txt }).Select(n => n.Key).ToList());
            }
            commonReviewList = commonReviewList.Where(n => n.Any(c => char.IsDigit(c) == false)).ToList();
            return Utility.CommonWords(commonReviewList, "ReviewFrequency");
        }

        /// <summary>
        /// Returns a dictionary of words based on how many assignments a words appears in
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Dictionary<string, int> WordsByAssignment(AppDbContext db)
        {
            var assignmentReviews = db.UserReviews.GroupBy(n => n.AssignmentID).ToList();
            int i = 0;
            List<string> commonAssiList = new List<string>();
            foreach (var assignmentReview in assignmentReviews)
            {
                var words = assignmentReview.Where(r => r.Comment != null).Select(r => new { Review = r.Comment, Lang = r.Language }).ToList();
                words.AddRange(assignmentReview.Where(r => r.Type == "textarea" && r.Answer != null).Select(r => new { Review = r.Answer, Lang = r.Language }).ToList());
                List<string> modifiedEnTexts = new();
                List<string> modifiedNoTexts = new();

                foreach (var review in words)
                {
                    var txt = review.Review.RemoveStopWords(review.Lang);
                    if (review.Lang == "en")
                        modifiedEnTexts.Add(txt);
                    else
                        modifiedNoTexts.Add(txt);
                }

                var thisTxtEn = Utility.GetWordAmount(modifiedEnTexts.ToArray());
                var thisTxtNo = Utility.GetWordAmount(modifiedNoTexts.ToArray());

                //Remove words with numbers
                thisTxtEn = thisTxtEn.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(x => x.Key, x => x.Value);
                thisTxtNo = thisTxtNo.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(x => x.Key, x => x.Value);

                commonAssiList.AddRange(thisTxtEn.Select(n => n.Key).ToList());
                //Utility.WriteResult("assiEn" + i.ToString(), thisTxtEn);
                //Utility.WriteResult("assiNo" + i.ToString(), thisTxtNo);
                i++;
            }

            return Utility.CommonWords(commonAssiList, "AssiFrequency");
        }

        /// <summary>
        /// Returns a dictionary of words based on how many users uses a word
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Dictionary<string, int> WordsByUser(AppDbContext db)
        {
            var reviews = db.UserReviews.GroupBy(r => r.UserReviewer);
            int i = 0;
            List<string> listEn = new List<string>();
            List<string> listNo = new List<string>();
            List<string> commonUserListEn = new List<string>();
            List<string> commonUserListNo = new List<string>();

            foreach (var userReview in reviews)
            {
                var words = userReview.Where(r => r.Comment != null).Select(r => new { Review = r.Comment, Lang = r.Language }).ToList();
                words.AddRange(userReview.Where(r => r.Type == "textarea" && r.Answer != null).Select(r => new { Review = r.Answer, Lang = r.Language }).ToList());
                List<string> modifiedEnTexts = new();
                List<string> modifiedNoTexts = new();

                foreach (var review in words)
                {
                    var txt = review.Review.RemoveStopWords(review.Lang);
                    if (review.Lang == "en")
                        modifiedEnTexts.Add(txt);
                    else
                        modifiedNoTexts.Add(txt);
                }

                var thisTxtEn = Utility.GetWordAmount(modifiedEnTexts.ToArray());
                var thisTxtNo = Utility.GetWordAmount(modifiedNoTexts.ToArray());

                //Remove words with numbers
                thisTxtEn = thisTxtEn.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(n => n.Key, n => n.Value);
                thisTxtNo = thisTxtNo.Where(n => n.Key.Any(c => char.IsDigit(c)) == false).ToDictionary(n => n.Key, n => n.Value);

                commonUserListEn.AddRange(thisTxtEn.Select(n => n.Key).ToList());
                commonUserListNo.AddRange(thisTxtNo.Select(n => n.Key).ToList());
                //Utility.WriteResult("userEn" + i.ToString(), thisTxtEn);
                //Utility.WriteResult("userNo" + i.ToString(), thisTxtNo);
                i++;
            }

            Utility.CommonWords(commonUserListNo, "UserCommonNo");
            return Utility.CommonWords(commonUserListEn, "UserCommonEn");
        }
    }
}
