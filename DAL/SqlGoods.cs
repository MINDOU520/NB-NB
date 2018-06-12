using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlGoods : IGoods
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        /// <summary>
        /// 获取前五条news
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Goods> GetAllGoods()
        {
            var goods = from m in db.Goods.OrderByDescending(p => p.GID) select m;

            return goods;
        }
        public IEnumerable<Goods> Getbasketball()
        {
            var goods = from m in db.Goods.Where(p => p.GBiaoshi.StartsWith("篮球")).OrderByDescending(p => p.GID) select m;

            return goods;
        }
        public IEnumerable<Goods> Getclothes()
        {
            var goods = from m in db.Goods.Where(p => p.GBiaoshi.StartsWith("球服")).OrderByDescending(p => p.GID) select m;

            return goods;
        }
        public IEnumerable<Goods> Getshose()
        {
            var goods = from m in db.Goods.Where(p => p.GBiaoshi.StartsWith("球鞋")).OrderByDescending(p => p.GID) select m;

            return goods;
        }
        public IEnumerable<Goods> Getprotective()
        {
            var goods = from m in db.Goods.Where(p => p.GBiaoshi.StartsWith("护具")).OrderByDescending(p => p.GID) select m;

            return goods;
        }
        public IEnumerable<Goods> Getother()
        {
            var goods = from m in db.Goods.Where(p => p.GBiaoshi.StartsWith("其他")).OrderByDescending(p => p.GID) select m;

            return goods;
        }
    }
}