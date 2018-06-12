using DALFactory;
using IDAL;
using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VideosManager
    {
        private static IVideos ivideos = (IVideos)DataAccess.Get<SqlVideos>();

        /// <summary>
        /// 根据时间获取前12个视频视频
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Video> GetVideos()
        {
            var videos = ivideos.GetVideosTop12();

            return videos;
        }

        /// <summary>
        /// 获取所有的视频
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Video> GetAllVideos()
        {
            var videos = ivideos.GetAllVideos();

            return videos;
        }
    }
}
