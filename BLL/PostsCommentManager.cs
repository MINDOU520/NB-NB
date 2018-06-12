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
    public class PostsCommentManager
    {
        private static IPostsComment iposts = (IPostsComment)DataAccess.Get<SqlPostsComment>();

        public IEnumerable<PostsComment> GetAllPostsComment()
        {
            var posts = iposts.GetAllPostsComment();

            return posts;
        }
    }
}

