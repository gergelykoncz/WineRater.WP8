using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using WineRater.Entities;
using WineRater.IoC;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly WineListViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = IoCContainer.Get<WineListViewModel>();
            DataContext = _viewModel;
        }

        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
            {
                return;
            }
            
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as Wine).WineId, UriKind.Relative));
            MainLongListSelector.SelectedItem = null;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SaveWinePage.xaml", UriKind.Relative));
        }
    }
}