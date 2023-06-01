using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTestingV2.APIModels
{
    public class TextReviews
    {
        [Key] public int Id { get; set; }
        [LoadColumn(0)]
        public string Review { get; set; }
        public int Course { get; set; }
        [LoadColumn(1)]
        [ColumnName("Label")]
        public float Score { get; set; }
        public int Grouping { get; set; }

        public TextReviews(string review, int course, float score, int grouping)
        {
            Review = review;
            Course = course;
            Score = score;
            Grouping=grouping;
        }
    }
}
