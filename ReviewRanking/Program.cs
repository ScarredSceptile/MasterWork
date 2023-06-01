using ReviewRanking.Models;

namespace ReviewRanking
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            string DefaultDbConnectionString = Directory.GetCurrentDirectory() + "/reviews.sqlite";
            ApplicationConfiguration.Initialize();

            if (File.Exists(Directory.GetCurrentDirectory + "/Settings.txt"))
            {
                //TODO: Add settings
                // Change ConnectionString
            }

            Application.Run(new Menu(DefaultDbConnectionString));
        }
    }
}