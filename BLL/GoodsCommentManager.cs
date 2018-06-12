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
    public class GoodsCommentManager
    {
        private static IGoodsComment igoods = (IGoodsComment)DataAccess.Get<SqlGoodsComment>();

        public IEnumerable<GoodsComment> GetAllGoodsComment()
        {
            var goods = igoods.GetAllGoodsComment();

            return goods;
        }
    }
}
