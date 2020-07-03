using KOTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class CourseViewPage
    {
        public string aa1 { get; set; }//显示列表
        public List<CourseInfo> aa { get; set; }//显示列表
        public IEnumerable<CourseViewPage> aaa { get; set; }
        public IEnumerable<CourseViewPage> CourseNameInfo { get; set; }
        public string CourseID { get; set; }
        public string CoursePic { get; set; }
        public string CourseName { get; set; }
        public string Class { get; set; }
        public string Material { get; set; }
        public string State { get; set; }
        public string Introduction { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
    }
}
