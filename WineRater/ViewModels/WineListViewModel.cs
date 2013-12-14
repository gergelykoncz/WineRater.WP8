using System.Collections.ObjectModel;
using WineRater.Entities;
using WineRater.Facade;
using WineRater.Infrastructure;

namespace WineRater.ViewModels
{
    public class WineListViewModel : ObservableModel
    {
        private readonly IWineFacade _wineFacade;
        private ObservableCollection<Wine> _wineList;

        public WineListViewModel(IWineFacade wineFacade)
        {
            this._wineFacade = wineFacade;
            fillListWithData();
            PubSub<Wine>.RegisterForEvent(PubSubEventNames.WineDeleted, WineDeletedEventHandler);
        }

        public ObservableCollection<Wine> WineList
        {
            get
            {
                return _wineList;
            }
            set
            {
                NotifyPropertyChanging("WineList");
                _wineList = value;
                NotifyPropertyChanged("WineList");
            }
        }

        private void fillListWithData()
        {
            var wines = _wineFacade.GetAllWines();
            WineList = new ObservableCollection<Wine>(wines);
        }

        private void WineDeletedEventHandler(Wine wine)
        {
            WineList.Remove(wine);
        }
    }
}
