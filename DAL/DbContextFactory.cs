using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class DbContextFactory
    { 
        public static NBNBEntities CreateDbContext()
        {
            NBNBEntities dbContext = (NBNBEntities)CallContext.GetData("dbContext");

            if (dbContext == null)
            {
            dbContext = new NBNBEntities();
            CallContext.SetData("dbContext", dbContext);
            }

            return dbContext;
        }
    }
}
