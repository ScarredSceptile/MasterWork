using ReviewRanking.Models;
using ReviewRanking.RankingResources;

namespace ReviewRanking
{
    public partial class Menu : Form
    {
        private string ConnectionString;

        public Menu(string connection)
        {
            InitializeComponent();
            ConnectionString = connection;
            SetConnection();
        }

        public void SetConnection()
        {

            if (File.Exists(ConnectionString) == false)
            {
                SetDBConnectionError("DB Not Found");
                return;
            }

            using (var _context = new AppDbContext(ConnectionString))
            {

                try
                {
                    var t = _context.TextReviews.Count();
                    //t = _context.RankingInfo.Count();
                }
                catch
                {
                    SetDBConnectionError("DB is Missing Table");
                    return;
                }

                if (_context.TextReviews.Count() == 0)
                {
                    SetDBConnectionError("Empty Database");
                    return;
                }
            }

            DbConnection.Text = "Connected";
            DbConnection.ForeColor= Color.Green;
            ShowStart();
        }

        private void SetDBConnectionError(string errorString)
        {
            DbConnection.Text = errorString;
            DbConnection.ForeColor = Color.Red;
            HideStart();
        }

        private void HideStart()
        {
            CourseSelecter.Enabled = false;
            StartRanking.Enabled = false;
            FixSavedData.Enabled = false;
        }

        private void ShowStart()
        {
            using (var _context = new AppDbContext(ConnectionString))
                CourseSelecter.DataSource = _context.TextReviews.GroupBy(n => n.Course).Select(n =>
                new Course
                {
                    ID = n.Key,
                    ReviewAmount = n.Count(),
                    CourseName = _context.Courses.Where(x => _context.CourseReviews.Where(z => z.Reviews == n.Key).Select(x => x.Course).First() == x.ID).Select(x => x.Name).First()
                }).ToArray();
            CourseSelecter.ValueMember = "ID";
            CourseSelecter.DisplayMember = "Show";
            CourseSelecter.Enabled = true;
            StartRanking.Enabled = true;
            FixSavedData.Enabled = true;
        }

        private void ChangeDbBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Directory.GetCurrentDirectory();
                dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    ConnectionString = dialog.FileName;
                }
                else return;
            }
            SetConnection();
        }

        private void StartRanking_Click(object sender, EventArgs e)
        {
            Ranking nextForm = new Ranking(ConnectionString, (int)CourseSelecter.SelectedValue);
            nextForm.ShowDialog();
        }

        private void FixSavedData_Click(object sender, EventArgs e)
        {
            StartRanking.Enabled = false;
            FixSavedData.Enabled = false;
            using (var _context = new AppDbContext(ConnectionString))
            {
                var folder = Path.GetDirectoryName(ConnectionString) + "/Data";
                if (Directory.Exists(folder) == false)
                {
                    StartRanking.Enabled = true;
                    FixSavedData.Enabled = true;
                    return;
                }

                var courses = _context.TextReviews.GroupBy(n => n.Course).ToArray();
                foreach (var course in courses)
                {
                    var revs = course.ToList();
                    Parallel.ForEach(revs, review =>
                    {
                        if (File.Exists(folder + $"/{review.Id}.txt"))
                        {
                            var comparisons = File.ReadAllLines(folder + $"/{review.Id}.txt").ToList();
                            var remove = new List<string>();
                            foreach (var comparison in comparisons)
                            {
                                if (course.Any(n => n.Id == int.Parse(comparison.Split(" ")[1])) == false)
                                {
                                    remove.Add(comparison);
                                }
                            }
                            if (remove.Count > 0)
                            {
                                comparisons = comparisons.Where(n => remove.Contains(n) == false).ToList();
                                File.WriteAllLines(folder + $"/{review.Id}.txt", comparisons);
                                File.WriteAllLines(folder + $"/Obsolete{review.Id}.txt", remove);
                            }
                        }
                    });
                }
            }
            StartRanking.Enabled = true;
            FixSavedData.Enabled = true;
        }
    }
}