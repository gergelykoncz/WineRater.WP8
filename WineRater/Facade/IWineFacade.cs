using System.Collections.Generic;
using WineRater.Entities;

namespace WineRater.Facade
{
    public interface IWineFacade
    {
        IEnumerable<Wine> GetAllWines();
        Wine GetWine(int wineId);

        void SaveWine(Wine wine);
        void RemoveWine(Wine wine);
    }
}
