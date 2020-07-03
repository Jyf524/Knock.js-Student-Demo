using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository
{
    [Table("ClassInfo")]
    public class ClassInfo
    {
        [Key]
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string Departments { get; set; }
        public string ProfessionName { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}
