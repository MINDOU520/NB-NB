using IDAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlMatch : IMatch
    {
        NBNBEntities db = DbContextFactory.CreateDbContext();

        public IEnumerable<Match> GetMatches()
        {
            var match = db.Match.OrderByDescending(n => n.MTime);

            return match;
        }
    }
}
