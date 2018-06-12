using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IGoods
    {
        IEnumerable<Goods> GetAllGoods();

        IEnumerable<Goods> Getbasketball();

        IEnumerable<Goods> Getclothes();

        IEnumerable<Goods> Getshose();

        IEnumerable<Goods> Getprotective();

        IEnumerable<Goods> Getother();
    }
}
