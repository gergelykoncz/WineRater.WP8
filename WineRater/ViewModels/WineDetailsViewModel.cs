using WineRater.Entities;
using WineRater.Facade;

namespace WineRater.ViewModels
{
    public class WineDetailsViewModel : ObservableModel
    {
        private readonly IWineFacade _wineFacade;
        private Wine _wine;

        public WineDetailsViewModel(IWineFacade wineFacade)
        {
            this._wineFacade = wineFacade;
        }

        public void SelectWine(int wineId)
        {
            Wine = _wineFacade.GetWine(wineId);
        }

        public void DeleteWine()
        {
            _wineFacade.RemoveWine(Wine);
        }

        public Wine Wine
        {
            get
            {
                return _wine;
            }
            set
            {
                if (_wine != value)
                {
                    NotifyPropertyChanging("Wine");
                    _wine = value;
                    NotifyPropertyChanged("Wine");
                }
            }
        }
    }
}
