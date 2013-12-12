using System.Collections.Generic;
using System.Linq;
using WineRater.Data;
using WineRater.Entities;

namespace WineRater.Facade
{
    public class WineFacade : IWineFacade
    {
        private readonly IWineRepository _wineRepository;

        public WineFacade(IWineRepository wineRepository)
        {
            this._wineRepository = wineRepository;
        }

        public IEnumerable<Wine> GetAllWines()
        {
            return _wineRepository.GetAllWines().OrderByDescending(x => x.DateAdded);
        }

        public Wine GetWine(int wineId)
        {
            return _wineRepository.GetWinesByCriteria(x => x.WineId == wineId).Single();
        }

        public void SaveWine(Wine wine)
        {
            _wineRepository.SaveWine(wine);
        }

        public void RemoveWine(Wine wine)
        {
            _wineRepository.RemoveWine(wine);
        }
    }
}
