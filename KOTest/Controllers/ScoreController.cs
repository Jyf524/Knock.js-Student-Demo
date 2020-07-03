using KOTest.Repository;
using KOTest.Repository.Constaint;
using KOTest.Service.Method;
using KOTest.Service.Model;
using Newtonsoft.Json;
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
    public class ScoreController : Controller
    {
        //
        // GET: /Score/
        public string TeacherIDSession { get; set; }
        protected DataContent db = new DataContent();
        public ActionResult Index()
        {
            try
            {
                TeacherIDSession = System.Web.HttpContext.Current.Session["UserName"].ToString();
                var TeacherID = db.TeacherInfo.Where(x => x.TeacherName == TeacherIDSession).FirstOrDefault().TeacherID;
                ScoreViewPage2 xx = new ScoreViewPage2();

                var Info = (from a in db.ScoreInfo
                            join b in db.StuInfo
                            on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                            on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID

                            select new ScoreViewPage2
                            {
                                CourseName = a.CourseName,
                                ClassName = b.StuClass
                            }).FirstOrDefault();

                var list = (from a in db.ScoreInfo
                            join b in db.StuInfo
                            on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                            on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID
                            where a.CourseName == Info.CourseName
                            where b.StuClass == Info.ClassName
                            select new ScoreViewPage2
                            {
                                StuID = b.StuID,
                                ScoreID = a.ScoreID,
                                ScoreName = a.ScoreName,
                                CourseName = a.CourseName,
                                StuIDCard = b.StuIDCard,

                                UsualScore = a.UsualScore,
                                MidtermScore = a.MidtermScore,

                                EndScore = a.EndScore,
                                Absence = a.Absence,

                                TotalScore = a.TotalScore,
                                State = a.State,

                            }).ToList();


                var pageSizeNum = 5;//每页显示多少条
                var ItemNum = list.Count();//数据总数
                var pageNum = (ItemNum % pageSizeNum) == 0 ? (ItemNum / pageSizeNum) : (ItemNum / pageSizeNum) + 1;//总页数
                var pageNumber = 1;
                xx.ItemNum = ItemNum.ToString();
                xx.pageNumx = pageNum;
                xx.pageNumber = pageNumber;
                xx.ClassName = Info.ClassName;
                xx.CourseName = Info.CourseName;
                xx.aa1 = JsonConvert.SerializeObject(list.OrderBy(x => x.ScoreName).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList());

                return View(xx);
            }

            catch
            {
                ScoreViewPage2 xx = new ScoreViewPage2();
                xx.ItemNum = "0";
                xx.pageNumx = 0;
                xx.pageNumber = 0;
                return View(xx);
            }
        }

        public ActionResult Index1(String Name, String CourseName, int? page)
        {
            try
            {
                TeacherIDSession = System.Web.HttpContext.Current.Session["UserName"].ToString();
                var TeacherID = db.TeacherInfo.Where(x => x.TeacherName == TeacherIDSession).FirstOrDefault().TeacherID;
                ScoreViewPage2 xx = new ScoreViewPage2();

                var list = (from a in db.ScoreInfo
                            join b in db.StuInfo
                                    on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                    on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID
                            select new ScoreViewPage2
                            {
                                StuID = b.StuID,
                                ScoreID = a.ScoreID,
                                ScoreName = a.ScoreName,
                                CourseName = a.CourseName,

                                StuIDCard = b.StuIDCard,
                                UsualScore = a.UsualScore,
                                MidtermScore = a.MidtermScore,

                                EndScore = a.EndScore,
                                Absence = a.Absence,

                                TotalScore = a.TotalScore,
                                State = a.State,

                            }).ToList();

                int pageNumber;
                int pageSizeNum;
                int ItemNum;
                int pageNum;
                if (!String.IsNullOrEmpty(Name))
                {
                    list = (from a in db.ScoreInfo
                            join b in db.StuInfo
                            on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                            on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID
                            where b.StuClass.Contains(Name)
                            select new ScoreViewPage2
                            {
                                StuID = b.StuID,
                                ScoreID = a.ScoreID,
                                ScoreName = a.ScoreName,
                                CourseName = a.CourseName,

                                StuIDCard = b.StuIDCard,
                                UsualScore = a.UsualScore,
                                MidtermScore = a.MidtermScore,

                                EndScore = a.EndScore,
                                Absence = a.Absence,

                                TotalScore = a.TotalScore,
                                State = a.State,

                            }).ToList();
                }
                if (!String.IsNullOrEmpty(CourseName))
                {
                    if (!String.IsNullOrEmpty(Name))
                    {
                        list = (from a in db.ScoreInfo
                                join b in db.StuInfo
                                on a.ScoreName equals b.StuName
                                join c in db.TeacherInfo
                                on a.TeacherID equals c.TeacherID
                                where a.TeacherID == TeacherID
                                where a.CourseName.Contains(CourseName)
                                where b.StuClass.Contains(Name)
                                select new ScoreViewPage2
                                {
                                    StuID = b.StuID,
                                    ScoreID = a.ScoreID,
                                    ScoreName = a.ScoreName,
                                    CourseName = a.CourseName,

                                    StuIDCard = b.StuIDCard,
                                    UsualScore = a.UsualScore,
                                    MidtermScore = a.MidtermScore,

                                    EndScore = a.EndScore,
                                    Absence = a.Absence,

                                    TotalScore = a.TotalScore,
                                    State = a.State,

                                }).ToList();
                    }
                    else
                    {
                        list = (from a in db.ScoreInfo
                                join b in db.StuInfo
                                on a.ScoreName equals b.StuName
                                join c in db.TeacherInfo
                                on a.TeacherID equals c.TeacherID
                                where a.TeacherID == TeacherID
                                where a.CourseName.Contains(CourseName)
                                select new ScoreViewPage2
                                {
                                    StuID = b.StuID,
                                    ScoreID = a.ScoreID,
                                    ScoreName = a.ScoreName,
                                    CourseName = a.CourseName,

                                    StuIDCard = b.StuIDCard,
                                    UsualScore = a.UsualScore,
                                    MidtermScore = a.MidtermScore,

                                    EndScore = a.EndScore,
                                    Absence = a.Absence,

                                    TotalScore = a.TotalScore,
                                    State = a.State,

                                }).ToList();
                    }
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

                list = list.OrderBy(x => x.ScoreName).Skip((pageNumber - 1) * pageSizeNum).Take(pageSizeNum).ToList();

                xx.aa = list;
                xx.pageNumber = pageNumber;
                xx.pageNumx = pageNum;
                xx.ItemNum = ItemNum.ToString();
                return Json(xx, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Content("请先登录");
            }


        }


        [HttpGet]
        public String SaveList(Array arrID, Array arr, Array arr1, Array arr2, Array arr3)
        {
            try
            {
                var strScoreIDVal = arrID.GetValue(0).ToString();
                var getScoreIDVal = strScoreIDVal.Split(',');

                var UsualScoreVal = arr.GetValue(0).ToString();
                var getUsualScoreVal = UsualScoreVal.Split(',');

                var MidtermScoreVal = arr1.GetValue(0).ToString();
                var getMidtermScoreVal = MidtermScoreVal.Split(',');

                var EndScoreVal = arr2.GetValue(0).ToString();
                var getEndScoreVal = EndScoreVal.Split(',');

                var Absence = arr3.GetValue(0).ToString();
                var getAbsence = Absence.Split(',');


                for (var i = 0; i < getScoreIDVal.Length; i++)
                {
                    var ScoreIDVal = getScoreIDVal.GetValue(i);
                    ScoreInfo ScoreInfo = db.ScoreInfo.Find(ScoreIDVal);
                    if (ScoreInfo.State != "Finish")
                    {
                        ScoreInfo.UsualScore = getUsualScoreVal[i];
                        ScoreInfo.MidtermScore = getMidtermScoreVal[i];
                        ScoreInfo.EndScore = getEndScoreVal[i];
                        ScoreInfo.Absence = getAbsence[i];
                        try
                        {
                            ScoreInfo.TotalScore = (Convert.ToInt32(getUsualScoreVal[i]) * 0.3 + Convert.ToInt32(getMidtermScoreVal[i]) * 0.3 + Convert.ToInt32(getEndScoreVal[i]) * 0.4).ToString();
                        }
                        catch
                        {
                            ScoreInfo.TotalScore = "0";
                        }
                        db.SaveChanges();
                    }
                }
                return "暂存成功";
            }
            catch
            {
                return "暂存失败";
            }
        }

        [HttpGet]
        public String SaveList1(Array arrID, Array arr, Array arr1, Array arr2, Array arr3)
        {
            try
            {
                var strScoreIDVal = arrID.GetValue(0).ToString();
                var getScoreIDVal = strScoreIDVal.Split(',');

                var UsualScoreVal = arr.GetValue(0).ToString();
                var getUsualScoreVal = UsualScoreVal.Split(',');

                var MidtermScoreVal = arr1.GetValue(0).ToString();
                var getMidtermScoreVal = MidtermScoreVal.Split(',');

                var EndScoreVal = arr2.GetValue(0).ToString();
                var getEndScoreVal = EndScoreVal.Split(',');

                var Absence = arr3.GetValue(0).ToString();
                var getAbsence = Absence.Split(',');


                for (var i = 0; i < getScoreIDVal.Length; i++)
                {
                    var ScoreIDVal = getScoreIDVal.GetValue(i);
                    ScoreInfo ScoreInfo = db.ScoreInfo.Find(ScoreIDVal);
                    if (ScoreInfo.State != "Finish")
                    {
                        ScoreInfo.UsualScore = getUsualScoreVal[i];
                        ScoreInfo.MidtermScore = getMidtermScoreVal[i];
                        ScoreInfo.EndScore = getEndScoreVal[i];
                        ScoreInfo.Absence = getAbsence[i];
                        ScoreInfo.State = "Finish";
                        try
                        {
                            ScoreInfo.TotalScore = (Convert.ToInt32(getUsualScoreVal[i]) * 0.3 + Convert.ToInt32(getMidtermScoreVal[i]) * 0.3 + Convert.ToInt32(getEndScoreVal[i]) * 0.4).ToString();
                        }
                        catch
                        {
                            ScoreInfo.TotalScore = "0";
                        }
                        db.SaveChanges();
                    }
                }
                return "提交成功";
            }
            catch
            {
                return "提交失败";
            }
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
                dbdata.Columns.Add("ScoreID");
                dbdata.Columns.Add("ScoreName");
                dbdata.Columns.Add("CourseName");
                dbdata.Columns.Add("UsualScore");
                dbdata.Columns.Add("MidtermScore");
                dbdata.Columns.Add("EndScore");
                dbdata.Columns.Add("Absence");
                dbdata.Columns.Add("TotalScore");
                dbdata.Columns.Add("State");
                dbdata.Columns.Add("AddTime");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["ScoreID"] = dr["编号"];
                    dr_["ScoreName"] = dr["姓名"];
                    dr_["CourseName"] = dr["课程名"];
                    dr_["UsualScore"] = dr["平时成绩"];
                    dr_["MidtermScore"] = dr["期中成绩"];
                    dr_["EndScore"] = dr["期末成绩"];
                    dr_["Absence"] = dr["缺勤课时数"];
                    dr_["TotalScore"] = dr["总分"];
                    dr_["State"] = dr["状态"];
                    dr_["AddTime"] = dr["添加时间"];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);

                string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];
                SqlBulkCopyByDatatable(constr, "ScoreInfo", dbdata);
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
                dbdata.Columns.Add("ScoreID1");
                dbdata.Columns.Add("ScoreName1");
                dbdata.Columns.Add("CourseName1");
                dbdata.Columns.Add("UsualScore1");
                dbdata.Columns.Add("MidtermScore1");
                dbdata.Columns.Add("EndScore1");
                dbdata.Columns.Add("Absence1");
                dbdata.Columns.Add("TotalScore1");
                dbdata.Columns.Add("State1");
                dbdata.Columns.Add("AddTime1");
                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["ScoreID1"] = dr["编号"];
                    dr_["ScoreName1"] = dr["姓名"];
                    dr_["CourseName1"] = dr["课程名"];
                    dr_["UsualScore1"] = dr["平时成绩"];
                    dr_["MidtermScore1"] = dr["期中成绩"];
                    dr_["EndScore1"] = dr["期末成绩"];
                    dr_["Absence1"] = dr["缺勤课时数"];
                    dr_["TotalScore1"] = dr["总分"];
                    dr_["State1"] = dr["状态"];
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

        public ActionResult ClassInfo()
        {
            try
            {
                ScoreViewPage2 xx = new ScoreViewPage2();

                TeacherIDSession = System.Web.HttpContext.Current.Session["UserName"].ToString();
                var TeacherID = db.TeacherInfo.Where(x => x.TeacherName == TeacherIDSession).FirstOrDefault().TeacherID;
                var list = (from a in db.ScoreInfo
                            join b in db.StuInfo
                            on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                            on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID
                            select new ScoreViewPage2
                            {
                                ClassName = b.StuClass
                            }).ToList();
                xx.aa = list.GroupBy(i=>i.ClassName).Select(i => new ScoreViewPage2
                  {
                      ClassName = i.Key,
                  }).ToList();;
                return Json(xx, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("");
            }
        }

        public ActionResult CourseInfo()
        {
            ScoreViewPage2 xx = new ScoreViewPage2();
            try
            {
                TeacherIDSession = System.Web.HttpContext.Current.Session["UserName"].ToString();
                var TeacherID = db.TeacherInfo.Where(x => x.TeacherName == TeacherIDSession).FirstOrDefault().TeacherID;
                var list = (from a in db.ScoreInfo
                            join b in db.StuInfo
                            on a.ScoreName equals b.StuName
                            join c in db.TeacherInfo
                            on a.TeacherID equals c.TeacherID
                            where a.TeacherID == TeacherID
                            select new ScoreViewPage2
                            {
                                CourseName = a.CourseName
                            }).ToList();
                xx.aa = list.GroupBy(i => i.CourseName).Select(i => new ScoreViewPage2
                {
                    CourseName = i.Key,
                }).ToList(); ;
                return Json(xx, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("");
            }
        }


    }
}
