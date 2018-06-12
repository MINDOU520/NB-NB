using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class OrderViewModel
    {
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        public IEnumerable <Orders> orders { get; set; }
    }
}