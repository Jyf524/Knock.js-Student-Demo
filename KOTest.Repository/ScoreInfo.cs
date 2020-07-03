using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository
{
    [Table("ScoreInfo")]
    public class ScoreInfo
    {
        [Key]
        public string ScoreID { get; set; }
        public string SutID { get; set; }
        public string ScoreName { get; set; }
        public string TeacherID { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string UsualScore { get; set; }
        public string MidtermScore { get; set; }
        public string EndScore { get; set; }
        public string Absence { get; set; }
        public string TotalScore { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}
