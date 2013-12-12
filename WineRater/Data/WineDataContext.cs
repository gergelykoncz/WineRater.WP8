using System.Data.Linq;
using WineRater.Entities;

namespace WineRater.Data
{
    public class WineDataContext : DataContext
    {
        private const string DBConnectionString = "Data Source=isostore:/WineRater.sdf";

        public WineDataContext() : base(DBConnectionString) { }

        public Table<Wine> Wines;
    }
}