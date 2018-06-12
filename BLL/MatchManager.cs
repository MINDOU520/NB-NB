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
    public class MatchManager
    {
        private static IMatch imatch = (IMatch)DataAccess.Get<SqlMatch>();

        public IEnumerable<Match> GetMatches()
        {
            var match = imatch.GetMatches();

            return match;
        }
    }
}
