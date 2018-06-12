using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class GoodsViewModel
    {
        public IEnumerable<Goods> Goods1 { get; set; }

        public IEnumerable<Goods> Basketball { get; set; }

        public IEnumerable<Goods> Clothes { get; set; }

        public IEnumerable<Goods> Shose { get; set; }

        public IEnumerable<Goods> Protective { get; set; }

        public IEnumerable<Goods> Other { get; set; }

        public Goods Goods2 { get; set; }

        public GoodsReply GoodsReply { get; set; }

        public GoodsComment GoodsComment { get; set; }

        public IEnumerable<GoodsComment> GoodsComment1 { get; set; }

        public IEnumerable<GoodsReply> GoodsReply1 { get; set; }
    }
}