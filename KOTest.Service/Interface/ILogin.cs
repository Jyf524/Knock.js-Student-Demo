using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOTest.Service.Interface
{
    public interface ILogin
    {
        String GetLogin(string UserName, string Password, string Code);
    }
}
