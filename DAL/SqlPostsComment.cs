using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlPostsComment : IPostsComment
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<PostsComment> GetAllPostsComment()
        {
            var postsComment = db.PostsComment.OrderByDescending(n => n.PCTime);

            return postsComment;
        }
    }
}
