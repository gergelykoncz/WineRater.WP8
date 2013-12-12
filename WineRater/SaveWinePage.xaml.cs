using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.IO;
using WineRater.Entities;
using WineRater.Facade;
using WineRater.IoC;

namespace WineRater
{
    public partial class SaveWinePage : PhoneApplicationPage
    {
        private byte[] _takenPhoto;

        public SaveWinePage()
        {
            InitializeComponent();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var facade = IoCContainer.Get<IWineFacade>();
            facade.SaveWine(new Wine() { Name = wineName.Text, Rating = (int)rating.Value, Picture = _takenPhoto });
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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