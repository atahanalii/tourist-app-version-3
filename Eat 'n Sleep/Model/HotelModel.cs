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
    class HotelModel : INotifyPropertyChanged
    {
        private string _address;
        private string _name;
        private string _description;
        private string _imageUrl;

        public HotelModel(string address, string name, string description, string imageUrl )
        {
            Address = address;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            private set { _imageUrl = value; }
        }

        public string Address
        {
            get { return _address; }
            private set { _address = value; }
        }

       
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            private set { _description = value; }
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
