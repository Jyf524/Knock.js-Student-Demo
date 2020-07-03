using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository
{
    [Table("SemInfo")]
    public class SemInfo
    {
        [Key]
        public string SemID { get; set; }
        public string SemName { get; set; }
        public string Semstatus { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}
