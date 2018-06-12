using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlGoodsComment : IGoodsComment
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<GoodsComment> GetAllGoodsComment()
        {
            var goodsComment = db.GoodsComment.OrderByDescending(n => n.GCTime);

            return goodsComment;
        }
    }
}
