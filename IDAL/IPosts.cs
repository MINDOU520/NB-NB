using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IPosts
    {
        IEnumerable<Posts> GetPosts();

        IEnumerable<Posts> GetAllPosts();
    }
}
