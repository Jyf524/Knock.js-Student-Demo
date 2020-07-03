using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository
{
     [Table("CourseInfo")]  
    public class CourseInfo
    {
            [Key]
            public string CourseID { get; set; }
            public string CoursePic { get; set; }
            public string CourseName { get; set; }
            public string Class { get; set; }
            public string Material { get; set; }
            public string State { get; set; }
            public string Introduction { get; set; }
            public Nullable<System.DateTime> AddTime { get; set; }
    }
}
