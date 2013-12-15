using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.IO;
using System.Windows.Navigation;
using WineRater.Entities;
using WineRater.Facade;
using WineRater.IoC;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class SaveWinePage : PhoneApplicationPage
    {
        private byte[] _takenPhoto;
        private readonly WineDetailsViewModel _viewModel;
        private int? _navigatedWineId;

        public SaveWinePage()
        {
            InitializeComponent();
            _viewModel = IoCContainer.Get<WineDetailsViewModel>();
            wineType.ItemsSource = Enum.GetValues(typeof(WineType));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string wineIdQueryParameter;
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out wineIdQueryParameter))
                {
                    int wineId = 0;
                    if (int.TryParse(wineIdQueryParameter, out wineId))
                    {
                        _navigatedWineId = wineId;
                        _viewModel.SelectWine(wineId);
                    }
                }
                else
                {
                    _viewModel.Wine = new Wine();
                }
                DataContext = _viewModel.Wine;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var wineToSave = this.DataContext as Wine;
            if (_takenPhoto != null)
            {
                wineToSave.Picture = _takenPhoto;
            }
            _viewModel.SaveWine();
            
            if (_navigatedWineId.HasValue)
            {
                string detailsPageUrl = string.Format("/DetailsPage.xaml?selectedItem={0}", _navigatedWineId.Value);
                NavigationService.Navigate(new Uri(detailsPageUrl, UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
        }

        protected void PhotoButton_Click(object sender, EventArgs e)
        {
            var task = new PhotoChooserTask();
            task.ShowCamera = true;
            task.Completed += task_Completed;
            task.Show();
        }

        private void task_Completed(object sender, PhotoResult e)
        {
            _takenPhoto = new byte[e.ChosenPhoto.Length];
            var targetStream = new MemoryStream(_takenPhoto);
            e.ChosenPhoto.CopyTo(targetStream);
        }
    }
}