﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface INewsComment
    {
        IEnumerable<NewsComment> GetAllNewsComment();
    }
}