using System;
using System.Collections.Generic;
using WineRater.Entities;
using WineRater.Facade;
using WineRater.Infrastructure;
using WineRater.Resources;

namespace WineRater.ViewModels
{
    public class WineDetailsViewModel : ObservableModel
    {
        private readonly IWineFacade _wineFacade;
        private Wine _wine;
        private List<string> _wineTypes;

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

        public IEnumerable<string> WineTypes
        {
            get
            {
                if (_wineTypes == null)
                {
                    _wineTypes = new List<string>();
                    foreach (var value in Enum.GetValues(typeof(WineType)))
                    {
                        _wineTypes.Add(LocalizedStrings.GetResourceForKey(string.Format("WineType{0}", value)));
                    }
                }
                return _wineTypes;
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