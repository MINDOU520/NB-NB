using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlNewsComment : INewsComment
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<NewsComment> GetAllNewsComment()
        {
            var newsComment = db.NewsComment.OrderByDescending(n => n.NCTime);

            return newsComment;
        }
    }
}
