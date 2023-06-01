using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewRanking.RankingResources
{
    public class Course
    {
        public int ID { get; set; }
        public int ReviewAmount { get; set; }
        public string CourseName { get; set; }
        public string Show { get => $"Course: {CourseName}, ID: {ID}, ReviewAmount: {ReviewAmount}";}
    }
}
