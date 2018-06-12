using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlVideos :IVideos
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<Video>GetVideosTop12()
        {
            var videos = db.Video.OrderByDescending(r => r.VTime).Take(12);     

            return videos;
        }

        public IEnumerable<Video>GetAllVideos()
        {
            var videos = db.Video.OrderByDescending(r => r.VTime);

            return videos;
        }

        public Video SelectID()
        {
            return null;
        }
    }
}
