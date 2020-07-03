using KOTest.Repository.Constaint;
using KOTest.Service.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class SelectClassController : Controller
    {
        //
        // GET: /SelectClass/

        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            SelectClassViewPage xx = new SelectClassViewPage();

            var list = db.SelectClass.ToList();
            var pageSizeNum = 5;//每页显示多少条
            var ItemNum = list.Count();//数据总数
            var pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            var pageNumber = 1;
            xx.ItemNum = ItemNum.ToString();
            xx.pageNumx = pageNum;
            xx.pageNumber = pageNumber;
            xx.aa1 = JsonConvert.SerializeObject(list.OrderByDescending(x => x.AddTime).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList());

            return View(xx);
        }

        public ActionResult Index1(String Name, String ClassNameSearch, String SearchString2, int? page)
        {
            SelectClassViewPage xx = new SelectClassViewPage();
            var list = db.SelectClass.ToList();
            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;
            if (!String.IsNullOrEmpty(Name))
            {
                list = list.Where(s => s.CourseName.Contains(Name)).ToList();
            }
            if (!String.IsNullOrEmpty(ClassNameSearch))
            {
                list = list.Where(s => s.ClassName.Contains(ClassNameSearch)).ToList();
            }
            if (!String.IsNullOrEmpty(SearchString2))
            {
                list = list.Where(s => s.ClassName.Contains(SearchString2)).ToList();
            }
          
            pageSizeNum = 5;//每页显示多少条
            ItemNum = list.Count();//数据总数
            pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            if (page == 4)
            {
                pageNumber = pageNum;
            }
            else
            {
                pageNumber = page ?? 1;
            }
            list = list.OrderByDescending(x => x.AddTime).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();
            xx.aa = list;
            xx.pageNumber = pageNumber;
            xx.pageNumx = pageNum;
            xx.ItemNum = ItemNum.ToString();
            return Json(xx, JsonRequestBehavior.AllowGet);
        }




        public ActionResult Index3(String SearchString)
        {
            ClassViewPage xx = new ClassViewPage();

            var type = db.ClassInfo.GroupBy(i => i.ProfessionName)
               .Select(i => new ClassViewPage
               {
                   ProfessionName = i.Key,
                   aaa = i.ToList()
               }).ToList();
            xx.bbb = type;
            //var ClassInfo = db.ClassInfo.Where(a => a.ClassName == null || a.ClassName == "").ToList();
            //var ClassSonInfo = db.ClassInfo.Where(a=>a.ClassName != null).ToList();
            //if (SearchString != null)
            //{
            //    var ClassInfo1 = db.ClassInfo.Where(a => a.ClassID == SearchString).FirstOrDefault();
            //    ClassSonInfo = ClassSonInfo.Where(s => s.ClassID == ClassInfo1.ClassID || s.ClassName != null).ToList();
            //}

            //xx.aaa = ClassInfo;
            //xx.bbb = ClassSonInfo;
            xx.SearchString = SearchString;
            return Json(xx, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClassInfo()
        {
            ClassViewPage xx = new ClassViewPage();
            var ClassInfo = db.ClassInfo.ToList();
            var ClassNameInfo = db.ClassInfo.GroupBy(i => i.ClassName)
              .Select(i => new ClassViewPage
              {
                  ClassName = i.Key,
              }).ToList();
            xx.ClassNameInfo = ClassNameInfo;
            return Json(xx, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CourseInfo()
        {
            CourseViewPage xx = new CourseViewPage();
            var CourseInfo = db.CourseInfo.ToList();
            var CourseNameInfo = db.CourseInfo.GroupBy(i => i.CourseName)
              .Select(i => new CourseViewPage
              {
                  CourseName = i.Key,
              }).ToList();
            xx.CourseNameInfo = CourseNameInfo;
            return Json(xx, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TeacherInfo()
        {
            TeacherViewPage xx = new TeacherViewPage();
            var TeacherInfo = db.TeacherInfo.ToList();
            xx.aa = TeacherInfo;
            return Json(xx, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public String SaveList(Array arr,String ID)
        {
            try
            {
                var str = arr.GetValue(0).ToString();
                var getArr = str.Split(',');

                for (var i = 0; i < getArr.Length; i++)
                {
                    var Teacher = getArr.GetValue(i);
                    TeacherInfo TeacherInfo = db.TeacherInfo.Find(Teacher);
                    var SelectClass = db.SelectClass.Where(s => s.SelectClassID.ToString() == ID).FirstOrDefault();

                    if (i == 0)
                    {
                        SelectClass.TeacherName = TeacherInfo.TeacherName;
                        SelectClass.TeacherID = TeacherInfo.TeacherID;
                    }
                    else
                    {

                        SelectClass.TeacherName += "," + TeacherInfo.TeacherName;
                        SelectClass.TeacherID += "," + TeacherInfo.TeacherID;
                    }
                    db.SaveChanges();
                }
                return ("任课成功");
            }
            catch
            {
                return ("任课失败");
            }
        }



        [HttpGet]
        public ActionResult IndexAddSave(SelectClassViewPage AddSave)
        {
            SelectClass SelectClassInfo = new SelectClass();
            SelectClassInfo.SelectClassID = DateTime.Now.ToString("yyyyMMddHHmmss");
            SelectClassInfo.ClassName = AddSave.ClassName;
            
            SelectClassInfo.CourseName = AddSave.CourseName;
            var CourseInfo = db.CourseInfo.Where(a => a.CourseName == AddSave.CourseName).FirstOrDefault();
            SelectClassInfo.CourseID = CourseInfo.CourseID;
            SelectClassInfo.TeacherID = "";
            SelectClassInfo.TeacherName = null; ;
            SelectClassInfo.WeeklyHours = AddSave.WeeklyHours;
            SelectClassInfo.CourseType = AddSave.CourseType;
            SelectClassInfo.WhetherExam = AddSave.WhetherExam;
            SelectClassInfo.AddTime = DateTime.Now;
            db.SelectClass.Add(SelectClassInfo);
            db.SaveChanges();
            return Json(SelectClassInfo, JsonRequestBehavior.AllowGet);
        }
    }
}
