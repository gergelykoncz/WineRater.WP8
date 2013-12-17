using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using WineRater.Entities;
using WineRater.IoC;
using WineRater.Resources;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly WineListViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            buildLocalizedApplicationBar();
            _viewModel = IoCContainer.Get<WineListViewModel>();
            DataContext = _viewModel;
        }

        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
            {
                return;
            }
            
            NavigationService.Navigate(new Uri("/Pages/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as Wine).WineId, UriKind.Relative));
            MainLongListSelector.SelectedItem = null;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SaveWinePage.xaml", UriKind.Relative));
        }

        private void buildLocalizedApplicationBar()
        {
            ApplicationBarIconButton addButton = new ApplicationBarIconButton(new Uri("/Assets/Images/Add.png", UriKind.Relative));
            addButton.Text = AppResources.AppBarAddText;
            addButton.Click += AddButton_Click;
            ApplicationBar.Buttons.Add(addButton);
        }
    }
}