using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public class DataAccess
    {
        private static string AssemblyName = ConfigurationManager.AppSettings["Path"].ToString();
        private static string db = ConfigurationManager.AppSettings["DB"].ToString();
       
        public static object Get<T>()
        {
            return Assembly.Load(AssemblyName).CreateInstance(typeof(T).ToString()); // 赋值时需作拆箱处理
        }
    }
}
