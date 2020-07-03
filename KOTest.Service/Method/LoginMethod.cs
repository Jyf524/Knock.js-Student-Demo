using KOTest.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Method
{
    public class LoginMethod : BaseRepository, ILogin
    {
        public String GetLogin(string UserName, string password, string code)
        {
            if (String.IsNullOrEmpty(UserName))
            {
                return "用户名不为空";
            }
            if (String.IsNullOrEmpty(password))
            {
                return "密码不为空";
            }
            if (String.IsNullOrEmpty(code))
            {
                return "验证码不为空";
            }


            var list = db.UserInfo.Where(x => x.UserName == UserName).ToList();


            if (list.Count() == 0)
            {
                return "用户名不存在！";
            }
            if (list.Count() == 0)
            {
                return "用户名或密码错误！";
            }
            if (list.FirstOrDefault().UserPassword != password)
            {
                return "密码错误！";
            }
            var UserInfo = db.UserInfo.Where(x => x.UserName == UserName).FirstOrDefault().UserRole;

            if (UserInfo != "Teacher")
            {
                return "身份错误！";
            }
            
            return "登录成功";
        }
    }
}
