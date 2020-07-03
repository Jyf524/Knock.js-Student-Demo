using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Constaint
{
    [Table("SelectClass")]
    public class SelectClass
    {
        [Key]
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
    }
}
