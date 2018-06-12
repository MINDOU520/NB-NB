using DAL;
using DALFactory;
using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsCommentManager
    {
        private static INewsComment inews = (INewsComment)DataAccess.Get<SqlNewsComment>();

        public IEnumerable<NewsComment> GetAllNewsComment()
        {
            var news = inews.GetAllNewsComment();

            return news;
        }
    }
}
