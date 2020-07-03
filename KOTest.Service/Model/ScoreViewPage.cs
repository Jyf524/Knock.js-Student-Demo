using KOTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class ScoreViewPage
    {
        
        public List<ScoreInfo> aa { get; set; }//显示列表

    }

    public class ScoreViewPage2
    {
        public string aa1 { get; set; }//显示列表

        public List<ScoreViewPage2> aa { get; set; }//显示列表

        public string ID { get; set; }
        public string StuID { get; set; }
        public string StuName { get; set; }
        public string StuIDCard { get; set; }
        public string StuIDClass { get; set; }

        public string ScoreID { get; set; }

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

        public string ClassName { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
    }
}
