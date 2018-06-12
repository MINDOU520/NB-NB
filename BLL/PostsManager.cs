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
    public class PostsManager
    {
        private static IPosts iposts = (IPosts)DataAccess.Get<SqlPosts>();

        public IEnumerable<Posts> GetPosts()
        {
            var posts = iposts.GetPosts();

            return posts;
        }

        public IEnumerable<Posts> GetAllPosts()
        {
            var posts = iposts.GetAllPosts();

            return posts;
        }
    }
}