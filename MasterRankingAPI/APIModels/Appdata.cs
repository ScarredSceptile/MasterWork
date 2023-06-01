namespace MasterRankingAPI.APIModels
{
    public class Appdata
    {
        public string UserName { get; set; }
        public string ConnectionToken { get; set; }
        public int SelectedCourse { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
