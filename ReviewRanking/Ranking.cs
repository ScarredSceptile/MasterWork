using ReviewRanking.Models;
using ReviewRanking.RankingResources;

namespace ReviewRanking
{
    public partial class Ranking : Form
    {
        private RankingProcess rankings;
        private TextReview LeftReview;
        private TextReview RightReview;
        private int Total;
        private int GroupTotal;

        public Ranking(string db, int course)
        {
            InitializeComponent();
            rankings = new RankingProcess(db, this, course);
            rankings.CompleteRanking();
        }

        public void SetReviews(TextReview leftReview, TextReview rightReview)
        {
            LeftReview = leftReview;
            RightReview = rightReview;
            ReviewLeft.Text = LeftReview.Review;
            ReviewRight.Text = RightReview.Review;
        }

        public void SetTotal(int total) { Total = total; }
        public void SetGroupTotal(int total) { GroupTotal = total; }
        public void SetTest(int value)
        {
            MethodProgress.Text = $"{value}/{Total}";
        }
        public void SetClicks(string clickValue)
        {
            Clicks.Text = clickValue;
        }

        public void SetCompareMethod(string method)
        {
            CompareMethod.Text = method;
        }

        public void StartLoading()
        {
            ReviewLeft.Hide();
            ReviewRight.Hide();
            LeftChoice.Hide();
            RightChoice.Hide();
            EqualValue.Hide();
            CompareMethod.Text = "Loading, please wait";
            time.Hide();
            Clicks.Hide();
            MethodProgress.Hide();
            GroupProgress.Hide();
        }

        public void StopLoading()
        {
            ReviewLeft.Show();
            ReviewRight.Show();
            LeftChoice.Show();
            RightChoice.Show();
            EqualValue.Show();
            MethodProgress.Show();
            GroupProgress.Show();
        }

        public void SetGroupProgress(int group)
        {
            GroupProgress.Text = $"Group {group}/{GroupTotal}";
        }

        public void SetTotalProgress(float progress)
        {
            GroupProgress.Text = $"{progress * 100}%";
        }

        public void HideGroupProgress()
        {
            GroupProgress.Hide();
        }

        public void SetTime(string text) { time.Text = "Time: " + text; }

        private void LeftChoice_Click(object sender, EventArgs e)
        {
            rankings.choice = Values.Left;
        }

        private void EqualValue_Click(object sender, EventArgs e)
        {
            rankings.choice = Values.Equal;
        }

        private void RightChoice_Click(object sender, EventArgs e)
        {
            rankings.choice = Values.Right;
        }

        private void EmptyData(object sender, FormClosingEventArgs e)
        {
            rankings = null;
        }
    }
}
