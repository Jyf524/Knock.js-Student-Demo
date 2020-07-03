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
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/

        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            TeacherViewPage xx = new TeacherViewPage();

            var list = db.TeacherInfo.Where(a => a.State == "Online").ToList();
            var pageSizeNum = 5;//每页显示多少条
            var ItemNum = list.Count();//数据总数
            var pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
            var pageNumber = 1;
            xx.ItemNum = ItemNum.ToString();
            xx.pageNumx = pageNum;
            xx.pageNumber = pageNumber;
            xx.aa1 = JsonConvert.SerializeObject(list.OrderBy(x => x.TeacherID).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList());

            return View(xx);
        }

        public ActionResult Index1(String Name, String Sex, int? page)
        {
            TeacherViewPage xx = new TeacherViewPage();
            var list = db.TeacherInfo.Where(a => a.State == "Online").ToList();
            int pageNumber;
            int pageSizeNum;
            int ItemNum;
            int pageNum;
            if (!String.IsNullOrEmpty(Name))
            {
                list = list.Where(s => s.TeacherName.Contains(Name)).ToList();
            }
            if (!String.IsNullOrEmpty(Sex))
            {
                list = list.Where(s => s.TeacherSex == Sex).ToList();
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
            list = list.OrderBy(x => x.TeacherID).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();
            xx.aa = list;
            xx.pageNumber = pageNumber;
            xx.pageNumx = pageNum;
            xx.ItemNum = ItemNum.ToString();
            return Json(xx, JsonRequestBehavior.AllowGet);
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
                    var Stu = getArr.GetValue(i);
                    TeacherInfo TeacherInfo = db.TeacherInfo.Find(Stu);
                    if (TeacherInfo.State == "Online")
                    {
                        TeacherInfo.State = "Outline";
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


        [HttpGet]
        public ActionResult IndexDetail(String id)
        {
            var contactInfo = db.TeacherInfo.Where(s => s.TeacherID.ToString() == id);
            return Json(contactInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IndexDetailSave(String id, String DetailName)
        {
            var contactInfo = db.TeacherInfo.Where(s => s.TeacherID.ToString() == id).FirstOrDefault();
            contactInfo.TeacherName = DetailName;
            db.SaveChanges();
            return Json(contactInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IndexAddSave(TeacherViewPage AddSave)
        {
            TeacherInfo TeacherInfo = new TeacherInfo();
            TeacherInfo.TeacherID = DateTime.Now.ToString("yyyyMMddHHmmss");
            TeacherInfo.TeacherName = AddSave.TeacherName;
            TeacherInfo.TeacherSex = AddSave.TeacherSex;
            TeacherInfo.TeacherCode = AddSave.TeacherCode;
            TeacherInfo.State = "Online";
            TeacherInfo.AddtTime = DateTime.Now;
            db.TeacherInfo.Add(TeacherInfo);
            db.SaveChanges();
            return Json(TeacherInfo, JsonRequestBehavior.AllowGet);
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
                dbdata.Columns.Add("TeacherID");
                dbdata.Columns.Add("TeacherName");
                dbdata.Columns.Add("TeacherSex");
                dbdata.Columns.Add("TeacherCode");
                dbdata.Columns.Add("State");
                dbdata.Columns.Add("AddtTime");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["TeacherID"] = dr["教工号"];
                    dr_["TeacherName"] = dr["教师姓名"];
                    dr_["TeacherSex"] = dr["教师性别"];
                    dr_["TeacherCode"] = dr["身份证号"];
                    dr_["State"] = dr["状态"];
                    dr_["AddtTime"] = dr["添加时间"];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);

                string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];
                SqlBulkCopyByDatatable(constr, "TeacherInfo", dbdata);
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
                dbdata.Columns.Add("TeacherID1");
                dbdata.Columns.Add("TeacherName1");
                dbdata.Columns.Add("TeacherSex1");
                dbdata.Columns.Add("TeacherCode1");
                dbdata.Columns.Add("State1");
                dbdata.Columns.Add("AddtTime1");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["TeacherID1"] = dr["教工号"];
                    dr_["TeacherName1"] = dr["教师姓名"];
                    dr_["TeacherSex1"] = dr["教师性别"];
                    dr_["TeacherCode1"] = dr["身份证号"];
                    dr_["State1"] = dr["状态"];
                    dr_["AddtTime1"] = dr["添加时间"];
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

        public ActionResult daochu()
        {
            //将查询出来的数据转化为对象列表的格式
            var dao = daochu1().aa;
            //创建工作簿
            HSSFWorkbook excelBook = new HSSFWorkbook();
            //为工作簿创建工作表并命名
            NPOI.SS.UserModel.ISheet sheet1 = excelBook.CreateSheet("学生信息");
            //创建表头
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);//先创建一行用来放表头
            //创建7列并赋值
            row1.CreateCell(0).SetCellValue("教师ID");//第0行，第1列
            row1.CreateCell(1).SetCellValue("教师姓名");//第0行，第2列
            row1.CreateCell(2).SetCellValue("教师性别");//第0行，第3列
            //创建数据行
            for (int i = 0; i < dao.Count(); i++)
            {

                //创建行
                NPOI.SS.UserModel.IRow rowTemp = sheet1.CreateRow(i + 1);//因为第一行已经被表头占用了，所以要+1
                rowTemp.CreateCell(0).SetCellValue(dao[i].TeacherID);
                rowTemp.CreateCell(1).SetCellValue(dao[i].TeacherName);
                rowTemp.CreateCell(2).SetCellValue(dao[i].TeacherSex);
            }
            //命名文件名
            var fileName = "教师信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";
            //将Excel表格转化为流，输出
            //创建文件流
            MemoryStream bookStream = new MemoryStream();
            //文件写入流（向流中写入字节序列）
            excelBook.Write(bookStream);
            //输出之前调用Seek（偏移量，游标位置) 把0位置指定为开始位置
            bookStream.Seek(0, SeekOrigin.Begin);
            return File(bookStream, "applicationnd.ms-excel", fileName);
        }

        public TeacherViewPage daochu1()
        {
            TeacherViewPage xx = new TeacherViewPage();
            var MessageInfo = db.TeacherInfo.ToList();
            xx.aa = MessageInfo;
            return xx;
        }

    }
}
