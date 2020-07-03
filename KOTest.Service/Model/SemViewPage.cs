using KOTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class SemViewPage
    {
        public string aa1 { get; set; }//显示列表
        public IEnumerable<SemInfo> aa { get; set; }//显示列表
        public string SemID { get; set; }
        public string SemName { get; set; }
        public string Semstatus { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
        public string searchstring;
    }
}
