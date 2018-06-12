using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlUsers : IUsers
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<Users > GetUsers()
        {
            var users = (from u in db.Users select u);

            return (users);
        }
    }
}
