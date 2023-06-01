using OpenAI_API;
using OpenAI_API.Completions;
using ReviewRanking.Models;
using ReviewRanking.RankingResources;
using System.Text.RegularExpressions;

namespace ReviewRanking
{
    public partial class Ranking : Form
    {
        private RankingProcess rankings;
        private TextReview LeftReview;
        private TextReview RightReview;
        private int Total;
        private int GroupTotal;
        private string folder;

        public Ranking(string db, int course)
        {
            InitializeComponent();
            folder = Path.GetDirectoryName(db);
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
            //Clicks.Text = clickValue;
        }

        public void SetCompareMethod(string method)
        {
            CompareMethod.Text = method;
        }

        public void StartLoading()
        {
            ReviewLeft.Hide();
            ReviewRight.Hide();
            ProgressBtn.Hide();
            CompareMethod.Text = "Loading, please wait";
            time.Hide();
            Clicks.Hide();
            MethodProgress.Hide();
            GroupProgress.Hide();
            GPTInput.Hide();
        }

        public void StopLoading()
        {
            ReviewLeft.Show();
            ReviewRight.Show();
            ProgressBtn.Show();
            MethodProgress.Show();
            GroupProgress.Show();
            Clicks.Show();
            GPTInput.Show();
            GPTInput.Enabled = false;
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

        private void EmptyData(object sender, FormClosingEventArgs e)
        {
            rankings = null;
        }

        public void Finish()
        {
            GroupProgress.Text = "Finished";
            ProgressBtn.Enabled = false;
        }

        private async void ProgressBtn_Click(object sender, EventArgs e)
        {
            /*
            ProgressBtn.Enabled = false;
            var completion = await CallChatGPT();
            if (completion)
                ProgressBtn.Enabled = true;
            */
            rankings.choice = GPTInput.Text;
            GPTInput.Text = "";
            GPTInput.Enabled = false;
        }

        public async Task<bool> CallChatGPT()
        {
            var key = File.ReadAllText(folder + "/key.txt");
            string answer = string.Empty;
            var openai = new OpenAIAPI(key);
            CompletionRequest request = new CompletionRequest();
            request.Prompt = $"Compare the following 2 code reviews for which review is better, or if they are equal, separated by an | after the colon and answer with only \"First\", \"Second\" or \"Equal\":{LeftReview.Review}|{RightReview.Review}";
            request.Model = OpenAI_API.Models.Model.DavinciText;
            request.MaxTokens = 100;

            Clicks.Text = "Sending API call";
            await Task.Delay(1);

            var completion = await openai.Completions.CreateCompletionsAsync(request);

            Clicks.Text = completion.Completions[0].Text;
            var res = completion.Completions[0].Text.ToLower();
            res = Regex.Replace(res, @"\s+", string.Empty);
            res = Regex.Replace(res, "[^a-zA-Z0-9 -]", string.Empty);
            File.WriteAllText(folder + "/test.txt", res);
            await Task.Delay(1);

            if (completion != null)
            {
                if (res == "first")
                {
                    rankings.choice = Values.Left;
                    return true;
                }
                if (res == "second")
                {
                    rankings.choice = Values.Right;
                    return true;
                }
                if (res == "equal")
                {
                    rankings.choice = Values.Equal;
                    return true;
                }
                else
                {
                    GPTInput.Enabled = true;
                    return false;
                }
            }
            else
            {
                GPTInput.Enabled = true;
                return false;
            }
            return true;
        }
    }
}
