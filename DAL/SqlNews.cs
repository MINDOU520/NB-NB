using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlNews : INews
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        /// <summary>
        /// 获取前五条news
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<News> GetNews()
        {
            var news = db.News.OrderByDescending(n => n.NTime).Take(5);

            return news;
        }

        /// <summary>
        /// 获取所有News
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetAllNews()
        {
            var news = db.News.OrderByDescending(n => n.NTime);

            return news;
        }
    }
}
