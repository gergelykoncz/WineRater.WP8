﻿using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using WineRater.IoC;
using WineRater.Resources;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private WineDetailsViewModel _viewModel;

        public DetailsPage()
        {
            InitializeComponent();
            buildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    _viewModel = IoCContainer.Get<WineDetailsViewModel>();
                    _viewModel.SelectWine(index);
                    DataContext = _viewModel;
                }
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MainPage.xaml", UriKind.Relative));
        }

        protected void EditWine_Click(object sender, EventArgs e)
        {
            int wineId = _viewModel.Wine.WineId;
            NavigationService.Navigate(new Uri(string.Format("/Pages/SaveWinePage.xaml?selectedItem={0}", wineId ), UriKind.Relative));
        }

        protected void DeleteWine_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(AppResources.DetailsPageDeleteText, AppResources.DetailsPageDeleteTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _viewModel.DeleteWine();
                NavigationService.Navigate(new Uri("/Pages/MainPage.xaml", UriKind.Relative));
            }
        }

        private void buildLocalizedApplicationBar()
        {
            ApplicationBarIconButton editButton = new ApplicationBarIconButton(new Uri("/Assets/Images/Edit.png", UriKind.Relative));
            editButton.Text = AppResources.AppBarEditText;
            editButton.Click += EditWine_Click;
            ApplicationBar.Buttons.Add(editButton);

            ApplicationBarIconButton deleteButton = new ApplicationBarIconButton(new Uri("/Assets/Images/Delete.png", UriKind.Relative));
            deleteButton.Text = AppResources.AppBarDeleteText;
            deleteButton.Click += DeleteWine_Click;
            ApplicationBar.Buttons.Add(deleteButton);
        }
    }
}