using System;
using System.Collections.Generic;
using WineRater.Entities;

namespace WineRater.Data
{
    public interface IWineRepository
    {
        IEnumerable<Wine> GetAllWines();
        IEnumerable<Wine> GetWinesByCriteria(Func<Wine, bool> criteria);
        void SaveWine(Wine wine);
        void RemoveWine(Wine wine);
    }
}
