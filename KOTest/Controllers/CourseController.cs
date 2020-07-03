using KOTest.Repository;
using KOTest.Repository.Constaint;
using KOTest.Service.Method;
using KOTest.Service.Model;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            CourseViewPage xx = new CourseViewPage();
            var list = db.CourseInfo.Where(a => a.State == "Online").ToList();
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

        public ActionResult Index1(String SearchString, String Type, String Department, int? page)
        {
            CourseViewPage xx = new CourseViewPage();
            var list = db.CourseInfo.Where(a => a.State == "Online").ToList();
            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;
            if (!String.IsNullOrEmpty(SearchString))
            {
                list = list.Where(s => s.CourseName.Contains(SearchString)|| s.CourseID.Contains(SearchString)).ToList();
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

        public ActionResult CourseAdd()
        {
            return View();
        }

        [HttpGet]
        public String DeleteList(Array arr)
        {
            try
            {
                var str = arr.GetValue(0).ToString();
                var getArr = str.Split(',');

                for (var i = 0; i < getArr.Length; i++)
                {
                    var Course = getArr.GetValue(i);
                    CourseInfo CourseInfo = db.CourseInfo.Find(Course);
                    if (CourseInfo.State == "Online")
                    {
                        CourseInfo.State = "Outline";
                        db.SaveChanges();
                    }
                }
                return ("删除成功");
            }
            catch
            {
                return ("删除失败");
            }
        }

        public bool IsImageUp = false;
        public static String Url { get; set; }
        [HttpPost]
        public ActionResult ImageSave()
        {
            string imgurl = null;
            if (Request.Files.Count > 0)
            {
                string paths = null;
                HttpPostedFileBase f = Request.Files["file"];
                string path = imgurl = "../Files/" + f.FileName;
                Url += "," + "/upload/" + path;
                paths = Server.MapPath(path);
                f.SaveAs(paths);
                IsImageUp = true;
            }
            return Json(imgurl);
        }


        [HttpGet]
        public ActionResult IndexAddSave(CourseViewPage AddSave)
        {
            CourseInfo CourseInfo = new CourseInfo();

            var aa = db.CourseInfo.Where(a => a.CourseID == AddSave.CourseID).Count();
            if (aa != 0)
            {
                return Content("编号不能重复");
            }
            if (!String.IsNullOrEmpty(AddSave.CourseID))
            {
                CourseInfo.CourseID = AddSave.CourseID;
            }
            else
            {
                return Content("编号不能为空");
            }
            if (!String.IsNullOrEmpty(AddSave.CourseName))
            {
                CourseInfo.CourseName = AddSave.CourseName;
            }
            else
            {
                return Content("名称不能为空");
            }
            if (!String.IsNullOrEmpty(AddSave.Class))
            {
                CourseInfo.Class = AddSave.Class;
            }
            else
            {
                return Content("课时不能为空");
            }
            if (!String.IsNullOrEmpty(AddSave.Material))
            {
                CourseInfo.Material = AddSave.Material;
            }
            else
            {
                return Content("教材不能为空");
            }
            if (!String.IsNullOrEmpty(AddSave.Introduction))
            {
                CourseInfo.Introduction = AddSave.Introduction;
            }
            else
            {
                CourseInfo.Introduction = "暂无简介";
            }
            CourseInfo.CoursePic = Url;
            Url = "";
            CourseInfo.State = "Online";
            CourseInfo.AddTime = DateTime.Now;
            db.CourseInfo.Add(CourseInfo);
            db.SaveChanges();
            return Content("添加成功");
        }

        [HttpPost]
        public ActionResult BatchAdd(HttpPostedFileBase file)
        {
            try
            {
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);

                DataTable excelTable = new DataTable();
                excelTable = ImportExcel.GetExcelDataTable(path);

                DataTable dbdata = new DataTable();
                dbdata.Columns.Add("CourseID");
                dbdata.Columns.Add("CourseName");
                dbdata.Columns.Add("FullName");
                dbdata.Columns.Add("FullNamepy");
                dbdata.Columns.Add("FullNameFrist");
                dbdata.Columns.Add("Department");
                dbdata.Columns.Add("State");
                dbdata.Columns.Add("Type");
                dbdata.Columns.Add("Introduction");
                dbdata.Columns.Add("AddTime");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["CourseID"] = dr["编号"];
                    dr_["CourseName"] = dr["名称"];
                    dr_["FullName"] = dr["全称"];
                    dr_["FullNamepy"] = dr["全拼"];
                    dr_["FullNameFrist"] = dr["首拼"];
                    dr_["Department"] = dr["系部"];
                    dr_["State"] = dr["状态"];
                    dr_["Type"] = dr["类型"];
                    dr_["Introduction"] = dr["简介"];
                    dr_["AddTime"] = dr["添加时间"];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);

                string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];
                SqlBulkCopyByDatatable(constr, "CourseInfo", dbdata);
                return Content("导入成功");
            }
            catch
            {
                return Content("导入失败");
            }
        }

        [HttpPost]
        public ActionResult BatchAdd2(HttpPostedFileBase file)
        {
            try
            {
                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);

                DataTable excelTable = new DataTable();
                excelTable = ImportExcel.GetExcelDataTable(path);

                DataTable dbdata = new DataTable();
                dbdata.Columns.Add("CourseID1");
                dbdata.Columns.Add("CourseName1");
                dbdata.Columns.Add("Class1");
                dbdata.Columns.Add("Material1");
                dbdata.Columns.Add("State1");
                dbdata.Columns.Add("Introduction1");
                dbdata.Columns.Add("AddTime1");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();



                    dr_["CourseID1"] = dr["编号"];
                    dr_["CourseName1"] = dr["课程名称"];
                    dr_["Class1"] = dr["课时"];
                    dr_["Material1"] = dr["材料"];
                    dr_["State1"] = dr["状态"];
                    dr_["Introduction1"] = dr["课程介绍"];
                    dr_["AddTime1"] = dr["添加时间"];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);
                var dataa = JsonConvert.SerializeObject(dbdata);
                return Json(dataa, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("导入失败");
            }
        }

        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dtSelect)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.BatchSize = 20000;
                        sqlbulkcopy.BulkCopyTimeout = 0;//不限时间
                        for (int i = 0; i < dtSelect.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dtSelect);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }

        [HttpGet]
        public ActionResult IndexDetail(String id)
        {
            var contactInfo = db.CourseInfo.Where(s => s.CourseID.ToString() == id);
            return Json(contactInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult IndexDetailSave(String id, String DetailName)
        {
            var contactInfo = db.CourseInfo.Where(s => s.CourseID.ToString() == id).FirstOrDefault();
            if (!String.IsNullOrEmpty(DetailName))
            {
                contactInfo.CourseName = DetailName;
            }
            else
            {
                return Content("名称不能为空");
            }
            db.SaveChanges();
            return Content("修改成功");
        }

        public ActionResult daochu()
        {
            //将查询出来的数据转化为对象列表的格式
            var dao = daochu1().aa;
            //创建工作簿
            HSSFWorkbook excelBook = new HSSFWorkbook();
            //为工作簿创建工作表并命名
            NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("课程信息");
            //创建表头
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);//先创建一行用来放表头
            //创建7列并赋值
            row1.CreateCell(0).SetCellValue("编号");//第0行，第0列
            row1.CreateCell(1).SetCellValue("课程名称");//第0行，第1列
            row1.CreateCell(2).SetCellValue("课时");//第0行，第2列
            row1.CreateCell(3).SetCellValue("材料");//第0行，第7列
            row1.CreateCell(4).SetCellValue("状态");//第0行，第8列
            row1.CreateCell(5).SetCellValue("课程介绍");//第0行，第9列
            row1.CreateCell(6).SetCellValue("添加时间");//第0行，第9列
            //创建数据行
            for (int i = 0; i < dao.Count(); i++)
            {
             
                //创建行
                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);//因为第一行已经被表头占用了，所以要+1
                rowTemp.CreateCell(0).SetCellValue(dao[i].CourseID);
                rowTemp.CreateCell(1).SetCellValue(dao[i].CourseName);
                rowTemp.CreateCell(2).SetCellValue(dao[i].Class);
                rowTemp.CreateCell(3).SetCellValue(dao[i].Material);
                rowTemp.CreateCell(4).SetCellValue(dao[i].State);
                rowTemp.CreateCell(5).SetCellValue(dao[i].Introduction);
                rowTemp.CreateCell(6).SetCellValue(Convert.ToDateTime(dao[i].AddTime));
            }
            //命名文件名
            var fileName = "课程信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";
            //将Excel表格转化为流，输出
            //创建文件流
            MemoryStream bookStream = new MemoryStream();
            //文件写入流（向流中写入字节序列）
            excelBook.Write(bookStream);
            //输出之前调用Seek（偏移量，游标位置) 把0位置指定为开始位置
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "applicationnd.ms-excel", fileName);
        }

        public CourseViewPage daochu1()
        {
            CourseViewPage xx = new CourseViewPage();
            var MessageInfo = db.CourseInfo.ToList();
            xx.aa = MessageInfo;
            return xx;
        }

       

    }
}
