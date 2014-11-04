using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Eat__n_Sleep.Annotations;

namespace Eat__n_Sleep.Model
{
    class RestaurantModel : INotifyPropertyChanged
    {
        private string _address;
        private string _name;
        private string _description;
        private string _imageUrl;

        public RestaurantModel(string address, string name, string description, string imageUrl)
        {
            throw new NotImplementedException(); 
            Address = address;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }


        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }


        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }



        public override string ToString()
        {
            return Name.ToString();
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
