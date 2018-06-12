using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class MatchViewModel
    {
        public Match match { get; set; }
        public IEnumerable<Entry> entries { get; set; }
    }
}