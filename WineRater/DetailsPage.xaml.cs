using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;
using WineRater.IoC;
using WineRater.ViewModels;

namespace WineRater
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        WineDetailsViewModel Model;

        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    Model = IoCContainer.Get<WineDetailsViewModel>();
                    Model.SelectWine(index);
                    DataContext = Model;
                }
            }
        }

        protected void DeleteWine_Click(object sender, EventArgs e)
        {
            Model.DeleteWine();
        }
    }
}