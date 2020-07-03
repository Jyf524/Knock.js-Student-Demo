using KOTest.Repository.Constaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class TeacherViewPage
    {
        public string aa1 { get; set; }//显示列表
        public List<TeacherInfo> aa { get; set; }//显示列表
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSex { get; set; }
        public string TeacherCode { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
    }
}
