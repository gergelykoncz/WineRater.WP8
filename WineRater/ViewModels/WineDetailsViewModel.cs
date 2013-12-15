using System;
using WineRater.Entities;
using WineRater.Facade;
using WineRater.Infrastructure;

namespace WineRater.ViewModels
{
    public class WineDetailsViewModel : ObservableModel
    {
        private readonly IWineFacade _wineFacade;
        private Wine _wine;

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

        public Array WineTypes
        {
            get
            {
                return Enum.GetValues(typeof(WineType));
            }
        }
        
        public WineDetailsViewModel(IWineFacade wineFacade)
        {
            this._wineFacade = wineFacade;
        }

        public void SelectWine(int wineId)
        {
            Wine = _wineFacade.GetWine(wineId);
        }

        public void SaveWine()
        {
            _wineFacade.SaveWine(Wine);
        }

        public void DeleteWine()
        {
            _wineFacade.RemoveWine(Wine);
            PubSub<Wine>.RaiseEvent(PubSubEventNames.WineDeleted, Wine);
        }
    }
}