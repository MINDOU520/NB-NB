using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlVideoComment : IVideoComment
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<VideoComment> GetAllVideoComment()
        {
            var videoComment = db.VideoComment.OrderByDescending(n => n.VCTime);

            return videoComment;
        }
    }
}

