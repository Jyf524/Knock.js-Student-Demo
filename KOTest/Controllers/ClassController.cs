using KOTest.Repository;
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
    public class ClassController : Controller
    {
        //
        // GET: /Class/

        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            ClassViewPage xx = new ClassViewPage();
            var list = db.ClassInfo.ToList();
            var pageSizeNum = 5;//每页显示多少条
            var ItemNum = list.Count();//数据总数
            var pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            var pageNumber = 1;
            xx.ItemNum = ItemNum.ToString();
            xx.pageNumx = pageNum;
            xx.pageNumber = pageNumber;
            xx.aa1 = JsonConvert.SerializeObject(list.OrderBy(x => x.AddTime).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList());
            return View(xx);
        }


        [HttpPost]
        public ActionResult ClassIndex(String Name, String status, int? page)
        {
            ClassViewPage xx = new ClassViewPage();
            var list = db.ClassInfo.ToList();
            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;
            if (!String.IsNullOrEmpty(Name))
            {
                list = list.Where(s => s.ClassName.Contains(Name)).ToList();
            }
            if (status != null && status != "全部")
            {
                list = list.Where(s => s.Departments.Contains(status)).ToList();
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
            list = list.OrderBy(x => x.AddTime).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();
            xx.aa = list;
            xx.pageNumber = pageNumber;
            xx.pageNumx = pageNum;
            xx.ItemNum = ItemNum.ToString();
            return Json(xx, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Classmove(String ID)
        {
            ClassInfo list = db.ClassInfo.Find(ID);
            db.ClassInfo.Remove(list);
            db.SaveChanges();
            return Json(list);
        }
        [HttpGet]
        public ActionResult ClassDetail(String id)
        {

            var contactInfo = db.ClassInfo.Where(s => s.ClassID.ToString() == id).FirstOrDefault();
            return Json(contactInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ClassDetailSave(String ID, String DetailName, String Detailstaus)
        {
            if (DetailName == "")
            {
                return Json("班级名称不能为空", JsonRequestBehavior.AllowGet);
            }
            if (Detailstaus == "")
            {
                return Json("所属系部不能为空", JsonRequestBehavior.AllowGet);
            }

            ClassInfo contacts = new ClassInfo();

            ClassInfo list = db.ClassInfo.Find(ID);
            list.ClassName = DetailName;
            list.Departments = Detailstaus;
            db.Entry(list).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("修改成功", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Classadd(String DetailName, String Detailstaus)
        {
            if (DetailName == "")
            {
                return Json("学期名称不能为空", JsonRequestBehavior.AllowGet);
            }
            if (Detailstaus == "")
            {
                return Json("学期状态不能为空", JsonRequestBehavior.AllowGet);
            }
            ClassInfo list = new ClassInfo();
            list.ClassID = DateTime.Now.ToString("yyyyMMssddff");
            list.ClassName = DetailName;
            list.Departments = Detailstaus;
            list.AddTime = DateTime.Now;
            db.ClassInfo.Add(list);
            db.SaveChanges();
            return Json("添加成功", JsonRequestBehavior.AllowGet);
        }

    }
}
