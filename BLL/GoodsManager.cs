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
    public class GoodsManager
    {
        private static IGoods igoods = (IGoods)DataAccess.Get<SqlGoods>();

        public IEnumerable<Goods> GetAllGoods()
        {
            var goods = igoods.GetAllGoods();

            return goods;
        }

        public IEnumerable<Goods> Getbasketball()
        {
            var goods = igoods.Getbasketball();

            return goods;
        }

        public IEnumerable<Goods> Getclothes()
        {
            var goods = igoods.Getclothes();

            return goods;
        }

        public IEnumerable<Goods> Getshose()
        {
            var goods = igoods.Getshose();

            return goods;
        }

        public IEnumerable<Goods> Getprotective()
        {
            var goods = igoods.Getprotective();

            return goods;
        }

        public IEnumerable<Goods> Getother()
        {
            var goods = igoods.Getother();

            return goods;
        }
    }
}
