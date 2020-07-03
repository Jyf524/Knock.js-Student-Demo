using KOTest.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Model
{
    public class IndexViewPage
    {
        public string aa1 { get; set; }//显示列表
        public List<StuInfo> aa { get; set; }//显示列表
        public string ID { get; set; }
        public string StuID { get; set; }
        public string StuName { get; set; }
        public string StuSex { get; set; }
        public string StuAge { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public string StuIDCard { get; set; }
        public string StuClass { get; set; }
        public int pageNumber;//当前页数
        public int pageNumx;//总页数
        public string ItemNum;
    }
}
