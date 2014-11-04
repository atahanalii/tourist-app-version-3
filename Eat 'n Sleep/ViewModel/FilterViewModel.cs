using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.Storage;
using Windows.UI.Popups;
using Eat__n_Sleep.Annotations;
using Eat__n_Sleep.Model;

namespace Eat__n_Sleep.ViewModel
{
    class FilterViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<RestaurantModel> AllRestaurants { get; set; }

        public ObservableCollection<RestaurantModel> FilteredRestaurants { get; set; }

        public String InputTextBox { get; set; }

        public ICommand filterCommand { get; set; }

        public FilterViewModel()
        {
            InputTextBox = "";

            AllRestaurants = new ObservableCollection<RestaurantModel>();
            FilteredRestaurants = new ObservableCollection<RestaurantModel>();

            FilteredRestaurants = AllRestaurants;

            LoadRestaurantModel();

            filterCommand = new Common.RelayCommand(FilterRestaurants);

        }

        private void FilterRestaurants()
        {
            new MessageDialog(AllRestaurants.Count().ToString()).ShowAsync();

           
            FilteredRestaurants = new ObservableCollection<RestaurantModel>();

            if (InputTextBox.Equals(""))
            {
                FilteredRestaurants = AllRestaurants;
            }
            else
            {
                foreach (RestaurantModel restaurantModel in AllRestaurants)
                {

                    if (restaurantModel.Name.Contains(InputTextBox))
                    {
                        FilteredRestaurants.Add(restaurantModel);
                    }
                }


            }
            OnPropertyChanged("FilteredRestaurants");

        }

        private async void LoadRestaurantModels()
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


            Stream restaurantStream = await file.OpenStreamForReadAsync();
            XDocument restaurantDocument = XDocument.Load(restaurantStream);

            IEnumerable<XElement> restaurantList = restaurantDocument.Descendants("restaurantmodel");

            foreach (XElement xElement in restaurantList)
            {
                RestaurantModel e = new RestaurantModel();
                e.Name = xElement.Element("name").Value;
                e.ImageUrl = xElement.Element("imageurl").Value;
                AllRestaurants.Add(e);
            }
            OnPropertyChanged("AllRestaurants");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
