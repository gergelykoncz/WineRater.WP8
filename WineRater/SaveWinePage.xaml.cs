using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.IO;
using System.Windows.Navigation;
using WineRater.Entities;
using WineRater.IoC;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class SaveWinePage : PhoneApplicationPage
    {
        private readonly WineDetailsViewModel _viewModel;
        private int? _navigatedWineId;

        public SaveWinePage()
        {
            InitializeComponent();
            _viewModel = IoCContainer.Get<WineDetailsViewModel>();
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
                DataContext = _viewModel;
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            _viewModel.SaveWine();
            finishEdit();
        }

        protected void PhotoButton_Click(object sender, EventArgs e)
        {
            var task = new PhotoChooserTask();
            task.ShowCamera = true;
            task.Completed += task_Completed;
            task.Show();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            finishEdit();
        }

        private void finishEdit()
        {
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

        private void task_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto != null)
            {
                //Copy the photo binary into a private byte array
                byte[] takenPhoto = new byte[e.ChosenPhoto.Length];
                var targetStream = new MemoryStream(takenPhoto);
                e.ChosenPhoto.CopyTo(targetStream);
                _viewModel.Wine.Picture = takenPhoto;
            }
        }
    }
}