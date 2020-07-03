using KOTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class ClassViewPage
    {
        public string aa1 { get; set; }//显示列表
        public IEnumerable<ClassInfo> aa { get; set; }//显示列表

        public IEnumerable<ClassInfo> aaa { get; set; }

        public IEnumerable<ClassViewPage> ClassNameInfo { get; set; }

        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string Departments { get; set; }
        public string ProfessionName { get; set; }


        public Nullable<System.DateTime> AddTime { get; set; }

        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
        public string SearchString { get; set; }
        public List<ClassViewPage> bbb { get; set; }
    }


}


