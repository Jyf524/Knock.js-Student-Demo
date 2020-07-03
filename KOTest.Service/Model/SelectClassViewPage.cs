using KOTest.Repository;
using KOTest.Repository.Constaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class SelectClassViewPage
    {
        public string aa1 { get; set; }//显示列表
        public List<SelectClass> aa { get; set; }//显示列表

        public IEnumerable<ClassInfo> aaa { get; set; }
        public IEnumerable<ClassInfo> bbb { get; set; }

        public string SelectClassID { get; set; }
        public string ClassName { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string WeeklyHours { get; set; }
        public string CourseType { get; set; }
        public string WhetherExam { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
    }
}
