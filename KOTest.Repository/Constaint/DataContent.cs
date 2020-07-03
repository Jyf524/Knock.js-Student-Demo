using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Repository.Constaint
{
    public partial class DataContent : DbContext
    {
        public DataContent()
            : base("DataBase")
        {

        }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<StuInfo> StuInfo { get; set; }
        public DbSet<SemInfo> SemInfo { get; set; }
        public DbSet<ClassInfo> ClassInfo { get; set; }
        public DbSet<CourseInfo> CourseInfo { get; set; }
        public DbSet<TeacherInfo> TeacherInfo { get; set; }
        public DbSet<SelectClass> SelectClass { get; set; }
        public DbSet<ScoreInfo> ScoreInfo { get; set; }
    }
}
