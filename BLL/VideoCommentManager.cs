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
    public class VideoCommentManager
    {
        private static IVideoComment ivideos = (IVideoComment)DataAccess.Get<SqlVideoComment>();

        public IEnumerable<VideoComment> GetAllVideoComment()
        {
            var videos = ivideos.GetAllVideoComment();

            return videos;
        }
    }
}
