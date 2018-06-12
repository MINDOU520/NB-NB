using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class CartViewModel
    {
        public List<ShoppingCart> shoppingCarts{get;set;}
        public ShoppingCart ShoppingCart { get; set; }
    }
}