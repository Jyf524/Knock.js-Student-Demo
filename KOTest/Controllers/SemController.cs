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
    public class SemController : Controller
    {
        //
        // GET: /Sem/
        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            SemViewPage xx = new SemViewPage();
            var list = db.SemInfo.ToList();
            var pageSizeNum = 5;//每页显示多少条
            var ItemNum = list.Count();//数据总数
            var pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            var pageNumber = 1;
            xx.ItemNum = ItemNum.ToString();
            xx.pageNumx = pageNum;
            xx.pageNumber = pageNumber;
            xx.aa1 = JsonConvert.SerializeObject(list.OrderBy(x => x.SemID).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList());

            return View(xx);
        }
        [HttpPost]
        public ActionResult SemIndex(String Name, String status, int? page)
        {
            SemViewPage xx = new SemViewPage();
            var list = db.SemInfo.ToList();
            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;
            if (!String.IsNullOrEmpty(Name))
            {
                list = list.Where(s => s.SemName.Contains(Name)).ToList();
            }
            if (status != null && status != "全部")
            {
                list = list.Where(s => s.Semstatus.Contains(status)).ToList();
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
            list = list.OrderBy(x => x.SemID).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();
            xx.aa = list;
            xx.pageNumber = pageNumber;
            xx.pageNumx = pageNum;
            xx.ItemNum = ItemNum.ToString();
            return Json(xx, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Semremove(String ID)
        {
            SemInfo bk = db.SemInfo.Find(ID);
            db.SemInfo.Remove(bk);
            db.SaveChanges();
            return Json(bk);
        }
        [HttpGet]
        public ActionResult SemDetail(String id)
        {

            var contactInfo = db.SemInfo.Where(s => s.SemID.ToString() == id).FirstOrDefault();
            return Json(contactInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SemDetailSave(String ID, String DetailName, String Detailstaus)
        {
            if (DetailName == "")
            {
                return Json("学期名称不能为空", JsonRequestBehavior.AllowGet);
            }
            if (Detailstaus == "")
            {
                return Json("学期状态不能为空", JsonRequestBehavior.AllowGet);
            }

            if (Detailstaus == "当前学期")
            {
                var aa = db.SemInfo.Where(a => a.Semstatus == "当前学期").Count();
                if (aa > 0)
                {
                    return Json("请修改当前学期", JsonRequestBehavior.AllowGet);
                }
            }

            SemInfo contacts = new SemInfo();

            SemInfo bk = db.SemInfo.Find(ID);
            bk.SemName = DetailName;
            bk.Semstatus = Detailstaus;
            db.Entry(bk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("修改成功", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Semadd(String DetailName, String Detailstaus)
        {
            if (DetailName == "")
            {
                return Json("学期名称不能为空", JsonRequestBehavior.AllowGet);
            }
            if (Detailstaus == "")
            {
                return Json("学期状态不能为空", JsonRequestBehavior.AllowGet);
            }
            SemInfo bk = new SemInfo();
            bk.SemID = DateTime.Now.ToString("yyyyMMssddff");
            bk.SemName = DetailName;
            bk.Semstatus = Detailstaus;
            bk.AddTime = DateTime.Now;
            db.SemInfo.Add(bk);
            db.SaveChanges();
            return Json("添加成功", JsonRequestBehavior.AllowGet);
        }

    }
}
