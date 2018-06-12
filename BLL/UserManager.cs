using DALFactory;
using DAL;
using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public  class UserManager
    {
        private static IUsers iuser = (IUsers)DataAccess.Get<SqlUsers>();

        public IEnumerable<Users> GetUsers()
        {
            var users = iuser.GetUsers();

            return users;
        }
    }
}
