using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Eat__n_Sleep.Annotations;
using Eat__n_Sleep.Model;

namespace Eat__n_Sleep.ViewModel
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RestaurantModel> _restaurant;
        private RestaurantModel _selectedRestaurantModel;
        private string _mainPageImg;

        public MainPageViewModel()
        {

            Restaurants = new ObservableCollection<RestaurantModel>();

            RestaurantModel restaurant1 = new RestaurantModel(
                "address",
                "name",
                "description",
                "imageUrl");

            Restaurants.Add(restaurant1);

        }

        public ObservableCollection<RestaurantModel> Restaurants
        {
            get { return _restaurant; }
            set { _restaurant = value; }
        }

        public string MainPageIMG
        {
            get { return _mainPageImg; }
            set { _mainPageImg = value; }
        }

        public RestaurantModel SelectedRestaurantModel
        {
            get { return _selectedRestaurantModel; }
            set
            {
                _selectedRestaurantModel = value;
                OnPropertyChanged("SelectedRestaurantModel");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



        private async void LoadRestaurants()
        {
            StorageFile file = null;
            try
            {
                file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("Restaurants.xml");
            }
            catch (Exception)
            {


            }
            if (file == null)
            {
                StorageFolder installationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                string xmlfile = @"Assets\xml\Models\Restaurants.xml";
                file = await installationFolder.GetFileAsync(xmlfile);
            }
            Stream RestaurantStream = await file.OpenStreamForReadAsync();
            XDocument restaurantDocument = XDocument.Load(RestaurantStream);


            IEnumerable<XElement> restaurantList = restaurantDocument.Descendants("restaurant");
            foreach (XElement xElement in restaurantList)
            {

                RestaurantModel R = new RestaurantModel(new Dictionary<string, string>(xElement.Element("Address"),""), );
                R.Description = xElement.Element("description").Value;
                R.Name = xElement.Element("name").Value;

            }
        }
    }
}