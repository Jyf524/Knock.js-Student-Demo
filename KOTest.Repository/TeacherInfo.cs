using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Constaint
{
     [Table("TeacherInfo")]
    public class TeacherInfo
    {
        [Key]
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSex { get; set; }
        public string TeacherCode { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> AddtTime { get; set; }
    }
}
