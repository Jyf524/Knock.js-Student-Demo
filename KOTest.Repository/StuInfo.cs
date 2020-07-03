using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository
{
    [Table("StuInfo")]
    public class StuInfo
    {
        [Key]
        public string StuID { get; set; }
        public string StuName { get; set; }
        public string StuSex { get; set; }
        public string StuAge { get; set; }
        public string StuIDCard { get; set; }
        public string StuClass { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> AddtTime { get; set; }
    }
}
