using KOTest.Service.Interface;
using KOTest.Service.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login1(string UserName, string PassWord, string Code)
        {
            ILogin Lo = new LoginMethod();
            string checkcode = Session["CheckCode"].ToString().ToLower();

            string check = Lo.GetLogin(UserName, PassWord, Code);
            if (check == "登录成功")
            {
                if (checkcode != Code.ToString().ToLower())
                {
                    return Json("验证码错误！");
                }
                System.Web.HttpContext.Current.Session["UserName"] = UserName;
            }
            return Json(check);

        }

    }
}
