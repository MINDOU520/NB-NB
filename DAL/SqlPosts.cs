using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlPosts : IPosts
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<Posts> GetPosts()
        {
            var posts = db.Posts.OrderByDescending(n => n.PTime).Take(5);

            return posts;
        }

        public IEnumerable<Posts> GetAllPosts()
        {
            var posts = db.Posts.OrderByDescending(n => n.PTime);

            return posts;
        }
    }
}
