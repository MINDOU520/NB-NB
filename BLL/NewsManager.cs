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
    public class NewsManager
    {
        private static INews inews = (INews)DataAccess.Get<SqlNews>();

        /// <summary>
        /// 获取前五条news
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetNews()
        {
            var news =inews.GetNews();

            return news;
        }

        /// <summary>
        /// 获取所有news
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetAllNews()
        {
            var news = inews.GetAllNews();

            return news;
        }
    }
}
